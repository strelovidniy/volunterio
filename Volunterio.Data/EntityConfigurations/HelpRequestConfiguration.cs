﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Data.EntityConfigurations;

internal class HelpRequestConfiguration : IEntityTypeConfiguration<HelpRequest>
{
    public void Configure(EntityTypeBuilder<HelpRequest> builder)
    {
        builder
            .ToTable(TableName.HelpRequests, TableSchema.Dbo);

        builder
            .HasKey(helpRequest => helpRequest.Id);

        builder
            .Property(helpRequest => helpRequest.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid);

        builder
            .Property(helpRequest => helpRequest.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc);

        builder
            .Property(helpRequest => helpRequest.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(helpRequest => helpRequest.DeletedAt)
            .IsRequired(false);

        builder
            .Property(helpRequest => helpRequest.IssuerId)
            .IsRequired();

        builder
            .Property(helpRequest => helpRequest.Tags)
            .HasConversion(
                tags => JsonConvert.SerializeObject(tags),
                json => JsonConvert.DeserializeObject<List<string>>(json) ?? Enumerable.Empty<string>()
            )
            .IsRequired();

        builder
            .Property(helpRequest => helpRequest.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(helpRequest => helpRequest.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder
            .Property(helpRequest => helpRequest.Deadline)
            .IsRequired(false);

        builder
            .Property(helpRequest => helpRequest.Latitude)
            .IsRequired(false);

        builder
            .Property(helpRequest => helpRequest.Longitude)
            .IsRequired(false);

        builder
            .Property(helpRequest => helpRequest.ShowContactInfo)
            .IsRequired();

        builder
            .HasMany(helpRequest => helpRequest.Images)
            .WithOne()
            .HasForeignKey(image => image.HelpRequestId)
            .HasPrincipalKey(helpRequest => helpRequest.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}