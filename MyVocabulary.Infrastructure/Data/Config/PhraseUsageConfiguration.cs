using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class PhraseUsageConfiguration : IEntityTypeConfiguration<PhraseUsage>
{
    public void Configure(EntityTypeBuilder<PhraseUsage> builder)
    {
        builder.Property(w => w.TopicId)
            .IsRequired();

        builder.Property(x => x.NativeSentence)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.TranslatedSentence)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PhotoUrl)
            .HasColumnType("text")
            .IsRequired(false);

        builder.HasIndex(w => w.NativePhraseId);

        builder.HasIndex(w => w.TranslationPhraseId);

        builder.HasOne<Phrase>()
            .WithMany()
            .HasForeignKey(w => w.NativePhraseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Phrase>()
            .WithMany()
            .HasForeignKey(w => w.TranslationPhraseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}