using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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
            helpRequest.Latitude = updateHelpRequestModel.Latitude;
            helpRequest.UpdatedAt = DateTime.UtcNow;
        }

        if (!helpRequest.Tags.SequenceEqual(updateHelpRequestModel.Tags))
        {
            helpRequest.Latitude = updateHelpRequestModel.Latitude;
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

        switch (currentUser.Role)
        {
            case { CanSeeHelpRequests: true, Type: not RoleType.Admin }:
                helpRequests = helpRequests.Where(helpRequest => helpRequest.DeletedAt == null);

                break;
            case { Type: RoleType.Admin }:
                break;
            default:
                helpRequests = helpRequests.Where(helpRequest => helpRequest.IssuerId == currentUser.Id);

                break;
        }

        if (!string.IsNullOrWhiteSpace(queryParametersModel.SearchQuery))
        {
            helpRequests = helpRequests.Where(helpRequest =>
                helpRequest.Tags.Contains(queryParametersModel.SearchQuery)
                || helpRequest.Title.Contains(queryParametersModel.SearchQuery)
                || helpRequest.Description.Contains(queryParametersModel.SearchQuery)
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
        await using var stream = new MemoryStream();

        var position = 0;

        foreach (var file in images)
        {
            ++position;

            await stream.FlushAsync(cancellationToken);

            stream.Position = 0;

            await file.CopyToAsync(stream, cancellationToken);

            stream.Position = 0;

            using var image = await Image.LoadAsync(stream, cancellationToken);

            if (image.Height > 1000 || image.Width > 1000)
            {
                image.Mutate(imageProcessingContext => imageProcessingContext.Resize(
                    new ResizeOptions
                    {
                        Size = new Size(1000, 1000),
                        Compand = true,
                        Mode = ResizeMode.Stretch,
                        Position = AnchorPositionMode.Center,
                        PadColor = Color.Black,
                        Sampler = KnownResamplers.Bicubic,
                        PremultiplyAlpha = false
                    }
                ));
            }

            await stream.FlushAsync(cancellationToken);

            stream.Position = 0;

            await image.SaveAsPngAsync(stream, cancellationToken);

            stream.Position = 0;

            var imageUrl = await storageService.SaveFileAsync(
                stream.ToArray(),
                FileExtension.Png,
                FolderName.Avatars,
                cancellationToken
            );

            if (image.Height > 100 || image.Width > 100)
            {
                image.Mutate(imageProcessingContext => imageProcessingContext.Resize(
                    new ResizeOptions
                    {
                        Size = new Size(100, 100),
                        Compand = true,
                        Mode = ResizeMode.Stretch,
                        Position = AnchorPositionMode.Center,
                        PadColor = Color.Black,
                        Sampler = KnownResamplers.Bicubic,
                        PremultiplyAlpha = false
                    }
                ));
            }

            await stream.FlushAsync(cancellationToken);

            stream.Position = 0;

            await image.SaveAsPngAsync(stream, cancellationToken);

            stream.Position = 0;

            var imageThumbnailUrl = await storageService.SaveFileAsync(
                stream.ToArray(),
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