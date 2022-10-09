using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailCore.Models.Configuration
{
    public class RecordConfiguration : IEntityTypeConfiguration<RecordSending>
    {
        public void Configure(EntityTypeBuilder<RecordSending> builder)
        {
            builder.Property(b => b.Result).HasConversion(c => c.ToString(), c => Enum.Parse<SendingStatus>(c));
        }
    }
}
