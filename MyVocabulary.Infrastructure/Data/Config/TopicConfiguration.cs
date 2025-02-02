using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data.Config;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(x => x.CultureFrom)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.CultureTo)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.Header)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.PhotoUrl)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Navigation(topic => topic.WordUsages)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(e => e.Header);

        builder.HasIndex(e => new { e.Header, e.CultureFrom, e.CultureTo });
    }
}