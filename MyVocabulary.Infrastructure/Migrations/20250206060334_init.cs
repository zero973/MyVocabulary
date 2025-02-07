using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVocabulary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phrases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Culture = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CultureFrom = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    CultureTo = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Header = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", maxLength: 300, nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhraseUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TopicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NativePhraseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TranslationPhraseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NativeSentence = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TranslatedSentence = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhraseUsages_Phrases_NativePhraseId",
                        column: x => x.NativePhraseId,
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhraseUsages_Phrases_TranslationPhraseId",
                        column: x => x.TranslationPhraseId,
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhraseUsages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PhraseUsageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRight = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_PhraseUsages_PhraseUsageId",
                        column: x => x.PhraseUsageId,
                        principalTable: "PhraseUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_Culture_Value",
                table: "Phrases",
                columns: new[] { "Culture", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_Value",
                table: "Phrases",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseUsages_NativePhraseId",
                table: "PhraseUsages",
                column: "NativePhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseUsages_TopicId",
                table: "PhraseUsages",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseUsages_TranslationPhraseId",
                table: "PhraseUsages",
                column: "TranslationPhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Header",
                table: "Topics",
                column: "Header");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Header_CultureFrom_CultureTo",
                table: "Topics",
                columns: new[] { "Header", "CultureFrom", "CultureTo" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_PhraseUsageId",
                table: "UserAnswers",
                column: "PhraseUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_PhraseUsageId_IsRight",
                table: "UserAnswers",
                columns: new[] { "PhraseUsageId", "IsRight" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "PhraseUsages");

            migrationBuilder.DropTable(
                name: "Phrases");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
