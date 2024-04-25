using Microsoft.AspNetCore.Http;

namespace Volunterio.Domain.Models.Set;

public record SetUserAvatarModel(
    IFormFile File
) : IValidatableModel;