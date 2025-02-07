using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class PhraseConfiguration : IEntityTypeConfiguration<Phrase>
{
    public void Configure(EntityTypeBuilder<Phrase> builder)
    {
        builder.Property(x => x.Culture)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Value);

        builder.HasIndex(e => new { e.Culture, e.Value })
            .IsUnique();
    }
}