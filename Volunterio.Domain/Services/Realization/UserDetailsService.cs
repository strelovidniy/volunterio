using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;

namespace Volunterio.Domain.Services.Realization;

internal class UserDetailsService(
    ICurrentUserService currentUserService,
    IRepository<UserDetails> userDetailsRepository,
    IRepository<Address> addressRepository,
    IStorageService storageService
) : IUserDetailsService
{
    public async Task UpdateAddressesAsync(
        UpdateAddressModel updateAddressModel,
        CancellationToken cancellationToken = default
    )
    {
        var userId = (await currentUserService.GetCurrentUserAsync(cancellationToken)).Id;

        var userDetails = await userDetailsRepository
            .Query()
            .Include(details => details.Address)
            .FirstOrDefaultAsync(
                details => details.UserId == userId,
                cancellationToken
            );

        if (userDetails is null)
        {
            userDetails = await userDetailsRepository.AddAsync(
                new UserDetails
                {
                    UserId = userId
                },
                cancellationToken
            );

            await userDetailsRepository.SaveChangesAsync(cancellationToken);
        }

        if (userDetails.Address is null)
        {
            var address = await addressRepository
                .AddAsync(
                    new Address
                    {
                        AddressLine1 = updateAddressModel.AddressLine1,
                        AddressLine2 = updateAddressModel.AddressLine2,
                        City = updateAddressModel.City,
                        State = updateAddressModel.State,
                        PostalCode = updateAddressModel.PostalCode,
                        Country = updateAddressModel.Country
                    },
                    cancellationToken
                );

            await addressRepository.SaveChangesAsync(cancellationToken);

            userDetails.AddressId = address.Id;
        }
        else
        {
            if (updateAddressModel.AddressLine1 != userDetails.Address.AddressLine1)
            {
                userDetails.Address.AddressLine1 = updateAddressModel.AddressLine1;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }

            if (updateAddressModel.AddressLine2 != userDetails.Address.AddressLine2)
            {
                userDetails.Address.AddressLine2 = updateAddressModel.AddressLine2;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }

            if (updateAddressModel.City != userDetails.Address.City)
            {
                userDetails.Address.City = updateAddressModel.City;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }

            if (updateAddressModel.State != userDetails.Address.State)
            {
                userDetails.Address.State = updateAddressModel.State;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }

            if (updateAddressModel.PostalCode != userDetails.Address.PostalCode)
            {
                userDetails.Address.PostalCode = updateAddressModel.PostalCode;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }

            if (updateAddressModel.Country != userDetails.Address.Country)
            {
                userDetails.Address.Country = updateAddressModel.Country;
                userDetails.Address.UpdatedAt = DateTime.UtcNow;
            }
        }

        await addressRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task SetUserAvatarAsync(
        SetUserAvatarModel model,
        CancellationToken cancellationToken = default
    )
    {
        var userId = (await currentUserService.GetCurrentUserAsync(cancellationToken)).Id;

        var userDetails = await userDetailsRepository
            .Query()
            .Include(details => details.Address)
            .FirstOrDefaultAsync(
                details => details.UserId == userId,
                cancellationToken
            );

        await using var stream = new MemoryStream();

        await stream.FlushAsync(cancellationToken);

        stream.Position = 0;

        await model.File.CopyToAsync(stream, cancellationToken);

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

        if (userDetails is null)
        {
            userDetails = await userDetailsRepository.AddAsync(
                new UserDetails
                {
                    UserId = userId,
                    ImageUrl = imageUrl,
                    ImageThumbnailUrl = imageThumbnailUrl
                },
                cancellationToken
            );
        }
        else
        {
            if (userDetails.ImageUrl != null)
            {
                storageService.RemoveFile(
                    userDetails.ImageUrl,
                    FolderName.Avatars,
                    cancellationToken
                );
            }

            userDetails.ImageUrl = imageUrl;
            userDetails.UpdatedAt = DateTime.UtcNow;

            if (userDetails.ImageThumbnailUrl != null)
            {
                storageService.RemoveFile(
                    userDetails.ImageThumbnailUrl,
                    FolderName.AvatarThumbnails,
                    cancellationToken
                );
            }

            userDetails.ImageThumbnailUrl = imageThumbnailUrl;
            userDetails.UpdatedAt = DateTime.UtcNow;
        }

        await userDetailsRepository.SaveChangesAsync(cancellationToken);
    }
}