using Humanizer;
using Volunterio.Data.Enums;

namespace Volunterio.Domain.Models;

public class ApiErrorResult(
    StatusCode statusCode
)
{
    public int StatusCode { get; init; } = (int) statusCode;

    public string Message { get; init; } = statusCode.ToString().Humanize();
}