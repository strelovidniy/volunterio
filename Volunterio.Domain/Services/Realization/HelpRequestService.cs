using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Exceptions;
using Volunterio.Domain.Extensions;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Models.Views;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Runtime;

namespace Volunterio.Domain.Services.Realization;

internal class HelpRequestService(
    IRepository<HelpRequest> helpRequestRepository,
    IRepository<HelpRequestImage> helpRequestImageRepository,
    ICurrentUserService currentUserService,
    IStorageService storageService,
    IImageService imageService,
    IMapper mapper,
    ILogger<HelpRequestService> logger
) : IHelpRequestService
{
    public async Task<HelpRequestView> GetHelpRequestAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var helpRequest = await helpRequestRepository
            .Query()
            .Include(request => request.Images)
            .Include(request => request.Issuer!.Details!.ContactInfo)
            .FirstOrDefaultAsync(
                request => request.Id == id,
                cancellationToken
            );

        RuntimeValidator.Assert(helpRequest is not null, StatusCode.HelpRequestNotFound);
        RuntimeValidator.Assert(helpRequest!.DeletedAt is null, StatusCode.HelpRequestRemoved);

        return mapper.Map<HelpRequestView>(helpRequest);
    }

    public async Task CreateHelpRequestAsync(
        CreateHelpRequestModel createHelpRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        var addedHelpRequest = await helpRequestRepository.AddAsync(
            new HelpRequest
            {
                Description = createHelpRequestModel.Description,
                Deadline = createHelpRequestModel.Deadline,
                IssuerId = currentUser.Id,
                Latitude = createHelpRequestModel.Latitude,
                Longitude = createHelpRequestModel.Longitude,
                ShowContactInfo = createHelpRequestModel.ShowContactInfo,
                Tags = createHelpRequestModel.Tags,
                Title = createHelpRequestModel.Title
            },
            cancellationToken
        );

        await helpRequestRepository.SaveChangesAsync(cancellationToken);

        await AddHelpRequestImagesAsync(
            createHelpRequestModel.Images,
            addedHelpRequest.Id,
            cancellationToken
        );
    }

    public async Task UpdateHelpRequestAsync(
        UpdateHelpRequestModel updateHelpRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        var helpRequest = await helpRequestRepository
            .Query()
            .Include(helpRequest => helpRequest.Images)
            .FirstOrDefaultAsync(
                helpRequest => helpRequest.Id == updateHelpRequestModel.Id,
                cancellationToken
            );

        RuntimeValidator.Assert(helpRequest is not null, StatusCode.HelpRequestNotFound);
        RuntimeValidator.Assert(helpRequest!.IssuerId == currentUser.Id, StatusCode.Forbidden);

        if (helpRequest.ShowContactInfo != updateHelpRequestModel.ShowContactInfo)
        {
            helpRequest.ShowContactInfo = updateHelpRequestModel.ShowContactInfo;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (helpRequest.Deadline != updateHelpRequestModel.Deadline)
        {
            helpRequest.Deadline = updateHelpRequestModel.Deadline;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (helpRequest.Title != updateHelpRequestModel.Title)
        {
            helpRequest.Title = updateHelpRequestModel.Title;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (helpRequest.Latitude != updateHelpRequestModel.Latitude)
        {
            helpRequest.Latitude = updateHelpRequestModel.Latitude;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (helpRequest.Longitude != updateHelpRequestModel.Longitude)
        {
            helpRequest.Longitude = updateHelpRequestModel.Longitude;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (!helpRequest.Tags?.SequenceEqual(updateHelpRequestModel.Tags) is true)
        {
            helpRequest.Tags = updateHelpRequestModel.Tags.ToList();
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        await helpRequestRepository.SaveChangesAsync(cancellationToken);

        if (updateHelpRequestModel.ImagesToDelete is not null)
        {
            var imagesToDelete = await helpRequestImageRepository
                .Query()
                .Where(
                    helpRequestImage => helpRequestImage.HelpRequestId == helpRequest.Id
                        && updateHelpRequestModel.ImagesToDelete.Contains(helpRequestImage.Id)
                )
                .ToListAsync(cancellationToken);

            await helpRequestImageRepository.DeleteRangeAsync(imagesToDelete, cancellationToken);
        }

        if (helpRequest.Images is not null && updateHelpRequestModel.ImagesToDelete is not null)
        {
            var idsToFixPosition = helpRequest
                .Images
                .Select(image => image.Id)
                .Except(updateHelpRequestModel.ImagesToDelete);

            var images = await helpRequestImageRepository
                .Query()
                .Where(image => idsToFixPosition.Contains(image.Id))
                .OrderBy(image => image.Position)
                .ToListAsync(cancellationToken);

            for (var i = 0; i < images.Count; i++)
            {
                images[i].Position = i + 1;
            }
        }

        await helpRequestImageRepository.SaveChangesAsync(cancellationToken);

        if (updateHelpRequestModel.Images is not null)
        {
            await AddHelpRequestImagesAsync(
                updateHelpRequestModel.Images,
                helpRequest.Id,
                cancellationToken
            );
        }
    }

    public async Task<PagedCollectionView<HelpRequestView>> GetHelpRequestsAsync(
        QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    )
    {
        var helpRequests = helpRequestRepository
            .Query()
            .AsNoTracking();

        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        helpRequests = helpRequests.Where(helpRequest => helpRequest.DeletedAt == null);

        if (currentUser.Role?.CanSeeHelpRequests is not true)
        {
            helpRequests = helpRequests.Where(helpRequest => helpRequest.IssuerId == currentUser.Id);
        }

        if (!string.IsNullOrWhiteSpace(queryParametersModel.SearchQuery))
        {
            var names = queryParametersModel.SearchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            helpRequests = helpRequests.Where(helpRequest =>
                (helpRequest.Tags as object as string)!.Contains(queryParametersModel.SearchQuery)
                || helpRequest.Title.Contains(queryParametersModel.SearchQuery)
                || helpRequest.Description.Contains(queryParametersModel.SearchQuery)
                || (names.Length == 2
                    && helpRequest.Issuer!.FirstName == names[0]
                    && helpRequest.Issuer.LastName == names[1])
            );
        }

        helpRequests = helpRequests.ExpandAndSort(queryParametersModel, logger);

        try
        {
            return new PagedCollectionView<HelpRequestView>(
                mapper.Map<IEnumerable<HelpRequestView>>(
                    await helpRequests
                        .Skip(queryParametersModel.PageSize * (queryParametersModel.PageNumber - 1))
                        .Take(queryParametersModel.PageSize)
                        .ToListAsync(cancellationToken)
                ),
                await helpRequests.CountAsync(cancellationToken)
            );
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorMessage.HelpRequestsGettingError);

            throw new ApiException(StatusCode.QueryResultError);
        }
    }

    public async Task DeleteHelpRequestAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var helpRequest = await helpRequestRepository
            .Query()
            .FirstOrDefaultAsync(
                request => request.Id == id,
                cancellationToken
            );

        RuntimeValidator.Assert(helpRequest is not null, StatusCode.HelpRequestNotFound);

        helpRequest!.DeletedAt = DateTime.UtcNow;

        await helpRequestRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task AddHelpRequestImagesAsync(
        IEnumerable<IFormFile> images,
        Guid helpRequestId,
        CancellationToken cancellationToken = default
    )
    {
        var lastImage = await helpRequestImageRepository
            .Query()
            .Where(image => image.HelpRequestId == helpRequestId)
            .OrderByDescending(image => image.Position)
            .FirstOrDefaultAsync(cancellationToken);

        var position = lastImage?.Position ?? 0;

        foreach (var file in images)
        {
            ++position;

            var resizedImageModel = await imageService.ResizeImageAsync(
                file,
                needThumbnail: true,
                keepAspectRatio: true,
                cancellationToken: cancellationToken
            );

            RuntimeValidator.Assert(
                resizedImageModel.ThumbnailImage is not null,
                StatusCode.ImageProcessingError
            );

            var imageUrl = await storageService.SaveFileAsync(
                resizedImageModel.ResizedImage,
                FileExtension.Png,
                FolderName.Avatars,
                cancellationToken
            );

            var imageThumbnailUrl = await storageService.SaveFileAsync(
                resizedImageModel.ThumbnailImage!,
                FileExtension.Png,
                FolderName.AvatarThumbnails,
                cancellationToken
            );

            await helpRequestImageRepository.AddAsync(
                new HelpRequestImage
                {
                    ImageUrl = imageUrl,
                    ImageThumbnailUrl = imageThumbnailUrl,
                    Position = position,
                    HelpRequestId = helpRequestId
                },
                cancellationToken
            );
        }

        await helpRequestImageRepository.SaveChangesAsync(cancellationToken);
    }
}