namespace Volunterio.Domain.Models.Views;

public record HelpRequestView(
    Guid Id,
    string Title,
    string Description,
    IEnumerable<string> Tags,
    double? Latitude,
    double? Longitude,
    ContactInfoView? ContactInfo,
    DateTime? Deadline,
    IEnumerable<HelpRequestImageView> Images
);