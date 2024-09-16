using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Separated_QuizAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_AssessmentSessions_SessionId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Assessments_AssessmentId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments");

            migrationBuilder.RenameTable(
                name: "Assessments",
                newName: "QuizAssessments");

            migrationBuilder.RenameIndex(
                name: "IX_Assessments_SessionId",
                table: "QuizAssessments",
                newName: "IX_QuizAssessments_SessionId");

            migrationBuilder.AddColumn<string>(
                name: "AudioContent",
                table: "QuizAssessments",
                type: "character varying(32768)",
                maxLength: 32768,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizAssessments",
                table: "QuizAssessments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuizAssessments_AssessmentId",
                table: "Questions",
                column: "AssessmentId",
                principalTable: "QuizAssessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAssessments_AssessmentSessions_SessionId",
                table: "QuizAssessments",
                column: "SessionId",
                principalTable: "AssessmentSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuizAssessments_AssessmentId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAssessments_AssessmentSessions_SessionId",
                table: "QuizAssessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizAssessments",
                table: "QuizAssessments");

            migrationBuilder.DropColumn(
                name: "AudioContent",
                table: "QuizAssessments");

            migrationBuilder.RenameTable(
                name: "QuizAssessments",
                newName: "Assessments");

            migrationBuilder.RenameIndex(
                name: "IX_QuizAssessments_SessionId",
                table: "Assessments",
                newName: "IX_Assessments_SessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_AssessmentSessions_SessionId",
                table: "Assessments",
                column: "SessionId",
                principalTable: "AssessmentSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Assessments_AssessmentId",
                table: "Questions",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
