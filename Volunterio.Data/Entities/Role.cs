﻿using EntityFrameworkCore.RepositoryInfrastructure;
using Volunterio.Data.Enums;

namespace Volunterio.Data.Entities;

public class Role : EntityBase, IEntity
{
    public string Name { get; set; } = null!;

    public bool IsHidden { get; set; }

    public RoleType Type { get; set; }

    public bool CanDeleteUsers { get; set; }

    public bool CanRestoreUsers { get; set; }

    public bool CanEditUsers { get; set; }

    public bool CanCreateRoles { get; set; }

    public bool CanEditRoles { get; set; }

    public bool CanDeleteRoles { get; set; }

    public bool CanSeeAllUsers { get; set; }

    public bool CanSeeUsers { get; set; }

    public bool CanSeeRoles { get; set; }

    public bool CanSeeAllRoles { get; set; }

    public bool CanMaintainSystem { get; set; }

    public IEnumerable<User>? Users { get; set; }
}