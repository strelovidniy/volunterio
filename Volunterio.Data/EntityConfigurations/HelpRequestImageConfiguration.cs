using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Data.EntityConfigurations;

internal class HelpRequestImageConfiguration : IEntityTypeConfiguration<HelpRequestImage>
{
    public void Configure(EntityTypeBuilder<HelpRequestImage> builder)
    {
        builder
            .ToTable(TableName.HelpRequestImages, TableSchema.Dbo);

        builder
            .HasKey(helpRequestImage => helpRequestImage.Id);

        builder
            .Property(helpRequestImage => helpRequestImage.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid);

        builder
            .Property(helpRequestImage => helpRequestImage.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc);

        builder
            .Property(helpRequestImage => helpRequestImage.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(helpRequestImage => helpRequestImage.DeletedAt)
            .IsRequired(false);

        builder
            .Property(helpRequestImage => helpRequestImage.ImageThumbnailUrl)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(helpRequestImage => helpRequestImage.ImageUrl)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(helpRequestImage => helpRequestImage.Position)
            .IsRequired();
    }
}