using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasIndex(e => e.WordUsageId);

        builder.HasIndex(e => new { e.WordUsageId, e.IsRight });

        builder.HasOne<WordUsage>()
            .WithMany()
            .HasForeignKey(w => w.WordUsageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}