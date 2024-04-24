using FluentValidation;
using Microsoft.AspNetCore.Http;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator(FileSize maxSize, IReadOnlyCollection<string> acceptedTypes)
    {
        RuleFor(file => file.Length)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.InvalidFile)
            .LessThanOrEqualTo(maxSize)
            .WithStatusCode(StatusCode.FileIsTooLarge);

        RuleFor(file => file.ContentType)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.InvalidFile)
            .Must(contentType => acceptedTypes.Any(type => type == contentType))
            .WithStatusCode(StatusCode.FileHasAnUnacceptableFormat);
    }
}