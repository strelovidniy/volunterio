using Microsoft.AspNetCore.Http;

namespace Volunterio.Domain.Models.Update;

public record UpdateHelpRequestModel(
    Guid Id,
    string Title,
    string Description,
    IEnumerable<string> Tags,
    double? Latitude = null,
    double? Longitude = null,
    bool ShowContactInfo = false,
    DateTime? Deadline = null,
    IEnumerable<IFormFile>? Images = null,
    IEnumerable<Guid>? ImagesToDelete = null
) : IValidatableModel;