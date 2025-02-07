﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyVocabulary.Infrastructure.Data;

#nullable disable

namespace MyVocabulary.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("MyVocabulary.Domain.Entities.Phrase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Value");

                    b.HasIndex("Culture", "Value")
                        .IsUnique();

                    b.ToTable("Phrases");
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.PhraseUsage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("NativePhraseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NativeSentence")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedSentence")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TranslationPhraseId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NativePhraseId");

                    b.HasIndex("TopicId");

                    b.HasIndex("TranslationPhraseId");

                    b.ToTable("PhraseUsages");
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CultureFrom")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("CultureTo")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("text");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Header");

                    b.HasIndex("Header", "CultureFrom", "CultureTo");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRight")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PhraseUsageId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PhraseUsageId");

                    b.HasIndex("PhraseUsageId", "IsRight");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.PhraseUsage", b =>
                {
                    b.HasOne("MyVocabulary.Domain.Entities.Phrase", null)
                        .WithMany()
                        .HasForeignKey("NativePhraseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyVocabulary.Domain.Entities.Topic", null)
                        .WithMany("PhraseUsages")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyVocabulary.Domain.Entities.Phrase", null)
                        .WithMany()
                        .HasForeignKey("TranslationPhraseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("MyVocabulary.Domain.Entities.PhraseUsage", null)
                        .WithMany()
                        .HasForeignKey("PhraseUsageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyVocabulary.Domain.Entities.Topic", b =>
                {
                    b.Navigation("PhraseUsages");
                });
#pragma warning restore 612, 618
        }
    }
}
