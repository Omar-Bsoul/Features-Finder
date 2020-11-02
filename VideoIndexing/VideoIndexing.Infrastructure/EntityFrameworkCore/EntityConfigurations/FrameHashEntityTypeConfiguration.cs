using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;

namespace VideoIndexing.Infrastructure.EntityFrameworkCore.EntityConfigurations {
    public class FrameHashEntityTypeConfiguration : IEntityTypeConfiguration<FrameHashBase> {
        public void Configure(EntityTypeBuilder<FrameHashBase> builder) {
            builder.ToTable("FrameHashes", Constants.DefaultDbSchema);

            builder.HasDiscriminator(p => p.Discriminator)
                .HasValue<OneFrameHash>(EnumFramingMode.OneFPS)
                .HasValue<FourFrameHash>(EnumFramingMode.FourFPS);
        }
    }
}
