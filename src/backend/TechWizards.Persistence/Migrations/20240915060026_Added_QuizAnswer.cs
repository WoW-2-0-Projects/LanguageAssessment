using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_QuizAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "OptionText",
                table: "Answers");

            migrationBuilder.AddColumn<long>(
                name: "Score",
                table: "QuizAssessments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "QuizAssessments",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrectOptionId",
                table: "Answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuizAssessmentId",
                table: "Answers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Result",
                table: "Answers",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "SubmittedOptionId",
                table: "Answers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OptionText = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CorrectOptionId",
                table: "Answers",
                column: "CorrectOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizAssessmentId",
                table: "Answers",
                column: "QuizAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SubmittedOptionId",
                table: "Answers",
                column: "SubmittedOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Options_CorrectOptionId",
                table: "Answers",
                column: "CorrectOptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Options_SubmittedOptionId",
                table: "Answers",
                column: "SubmittedOptionId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuizAssessments_QuizAssessmentId",
                table: "Answers",
                column: "QuizAssessmentId",
                principalTable: "QuizAssessments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Options_CorrectOptionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Options_SubmittedOptionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuizAssessments_QuizAssessmentId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CorrectOptionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuizAssessmentId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_SubmittedOptionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "QuizAssessments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "QuizAssessments");

            migrationBuilder.DropColumn(
                name: "CorrectOptionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizAssessmentId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "SubmittedOptionId",
                table: "Answers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OptionText",
                table: "Answers",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }
    }
}
