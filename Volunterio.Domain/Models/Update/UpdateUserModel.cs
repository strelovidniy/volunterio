﻿namespace Volunterio.Domain.Models.Update;

public record UpdateUserModel(
    Guid Id,
    string? FirstName,
    string? LastName
) : IValidatableModel;