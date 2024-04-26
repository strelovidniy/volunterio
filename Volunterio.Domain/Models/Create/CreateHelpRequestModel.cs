using Microsoft.AspNetCore.Http;

namespace Volunterio.Domain.Models.Create;

public record CreateHelpRequestModel(
    string Title,
    string Description,
    IEnumerable<string> Tags,
    double? Latitude,
    double? Longitude,
    bool ShowContactInfo,
    DateTime? Deadline,
    IEnumerable<IFormFile> Images
) : IValidatableModel;