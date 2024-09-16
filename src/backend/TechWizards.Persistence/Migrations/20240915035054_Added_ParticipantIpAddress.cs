using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_ParticipantIpAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantIpAddress",
                table: "AssessmentSessions",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantIpAddress",
                table: "AssessmentSessions");
        }
    }
}
