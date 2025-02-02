using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.Property(x => x.Culture)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(40);

        builder.HasIndex(e => e.Value);

        builder.HasIndex(e => new { e.Culture, e.Value })
            .IsUnique();
    }
}