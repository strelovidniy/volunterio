using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Data.EntityConfigurations
{
    internal class NotificationSettingConfiguration : IEntityTypeConfiguration<NotificationSetting>
    {
        public void Configure(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder
                .ToTable(TableName.NotificationSettings, TableSchema.Dbo);

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
        }
    }
}
