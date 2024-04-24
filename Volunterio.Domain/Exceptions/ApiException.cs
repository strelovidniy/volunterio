using Volunterio.Data.Enums;

namespace Volunterio.Domain.Exceptions;

public class ApiException(StatusCode statusCode) : Exception
{
    public StatusCode StatusCode { get; } = statusCode;
}