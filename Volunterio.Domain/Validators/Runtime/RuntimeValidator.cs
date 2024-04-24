﻿using Volunterio.Data.Enums;
using Volunterio.Domain.Exceptions;

namespace Volunterio.Domain.Validators.Runtime;

public static class RuntimeValidator
{
    public static void Assert(bool condition, StatusCode statusCode) =>
        CheckAndThrow(condition, new ApiException(statusCode));


    public static void CheckAndThrow<T>(bool condition, T exception) where T : Exception
    {
        if (!condition)
        {
            throw exception;
        }
    }
}