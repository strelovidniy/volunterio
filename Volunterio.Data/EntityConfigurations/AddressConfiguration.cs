using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Data.EntityConfigurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .ToTable(TableName.Addresses, TableSchema.Dbo);

        builder
            .HasKey(address => address.Id);

        builder
            .Property(address => address.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid);

        builder
            .Property(address => address.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc);

        builder
            .Property(address => address.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(address => address.DeletedAt)
            .IsRequired(false);

        builder
            .Property(address => address.AddressLine1)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(address => address.AddressLine2)
            .IsRequired(false)
            .HasMaxLength(200);

        builder
            .Property(address => address.City)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(address => address.State)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(address => address.PostalCode)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(address => address.Country)
            .IsRequired()
            .HasMaxLength(200);
    }
}