﻿using Microsoft.EntityFrameworkCore;
using Volunterio.Data.EntityConfigurations;
using Volunterio.Data.Extensions;

namespace Volunterio.Data.Context;

public class VolunterioContext : DbContext
{
    public VolunterioContext(DbContextOptions<VolunterioContext> options) : base(options)
    {
    }

    public VolunterioContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}