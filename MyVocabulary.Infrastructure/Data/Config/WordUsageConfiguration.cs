using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class WordUsageConfiguration : IEntityTypeConfiguration<WordUsage>
{
    public void Configure(EntityTypeBuilder<WordUsage> builder)
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

        builder.HasIndex(w => w.NativeWordId);

        builder.HasIndex(w => w.TranslationWordId);

        builder.HasOne<Word>()
            .WithMany()
            .HasForeignKey(w => w.NativeWordId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Word>()
            .WithMany()
            .HasForeignKey(w => w.TranslationWordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}