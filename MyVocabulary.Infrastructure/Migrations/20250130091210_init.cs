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
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Culture = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TopicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NativeWordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TranslationWordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NativeSentence = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TranslatedSentence = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordUsages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordUsages_Words_NativeWordId",
                        column: x => x.NativeWordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordUsages_Words_TranslationWordId",
                        column: x => x.TranslationWordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WordUsageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRight = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_WordUsages_WordUsageId",
                        column: x => x.WordUsageId,
                        principalTable: "WordUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Header",
                table: "Topics",
                column: "Header");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_Header_CultureFrom_CultureTo",
                table: "Topics",
                columns: new[] { "Header", "CultureFrom", "CultureTo" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_WordUsageId",
                table: "UserAnswers",
                column: "WordUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_WordUsageId_IsRight",
                table: "UserAnswers",
                columns: new[] { "WordUsageId", "IsRight" });

            migrationBuilder.CreateIndex(
                name: "IX_Words_Culture_Value",
                table: "Words",
                columns: new[] { "Culture", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_Value",
                table: "Words",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_WordUsages_NativeWordId",
                table: "WordUsages",
                column: "NativeWordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordUsages_TopicId",
                table: "WordUsages",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_WordUsages_TranslationWordId",
                table: "WordUsages",
                column: "TranslationWordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "WordUsages");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
