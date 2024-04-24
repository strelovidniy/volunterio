using Microsoft.EntityFrameworkCore;
using Volunterio.Data.Entities;
using Volunterio.Data.Extensions.Seed;

namespace Volunterio.Data.Extensions;

internal static class SeedDataExtension
{
    public static void Seed(this ModelBuilder builder)
    {
        Roles.Seed(builder.Entity<Role>());
        Users.Seed(builder.Entity<User>());
    }
}