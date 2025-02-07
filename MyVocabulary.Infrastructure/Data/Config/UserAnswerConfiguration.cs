using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasIndex(e => e.PhraseUsageId);

        builder.HasIndex(e => new { e.PhraseUsageId, e.IsRight });

        builder.HasOne<PhraseUsage>()
            .WithMany()
            .HasForeignKey(w => w.PhraseUsageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}