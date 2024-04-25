﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Data.EntityConfigurations;

internal class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder
            .ToTable(TableName.UserDetails, TableSchema.Dbo);

        builder
            .HasKey(userDetails => userDetails.Id);

        builder
            .Property(userDetails => userDetails.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid);

        builder
            .Property(userDetails => userDetails.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc);

        builder
            .Property(userDetails => userDetails.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(userDetails => userDetails.DeletedAt)
            .IsRequired(false);

        builder
            .Property(userDetails => userDetails.ImageUrl)
            .HasMaxLength(200)
            .IsRequired(false);

        builder
            .Property(userDetails => userDetails.ImageThumbnailUrl)
            .HasMaxLength(200)
            .IsRequired(false);

        builder
            .Property(userDetails => userDetails.AddressId)
            .IsRequired(false);

        builder
            .HasOne(userDetails => userDetails.Address)
            .WithMany()
            .HasForeignKey(userDetails => userDetails.AddressId)
            .HasPrincipalKey(address => address.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}