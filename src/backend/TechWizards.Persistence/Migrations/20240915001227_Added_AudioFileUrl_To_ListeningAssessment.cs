using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_AudioFileUrl_To_ListeningAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioFileUrl",
                table: "QuizAssessments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioFileUrl",
                table: "QuizAssessments");
        }
    }
}
