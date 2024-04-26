namespace Volunterio.Domain.Models.Views;

public record HelpRequestView(
    Guid Id,
    string Title,
    string Description,
    IEnumerable<string> Tags,
    double? Latitude,
    double? Longitude,
    bool ShowContactInfo,
    DateTime? Deadline,
    IEnumerable<HelpRequestImageView> Images
);