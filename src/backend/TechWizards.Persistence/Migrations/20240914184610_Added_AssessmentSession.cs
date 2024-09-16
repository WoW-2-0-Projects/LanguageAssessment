using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_AssessmentSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "Assessments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Assessments",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Assessments",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AssessmentSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Step = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSessions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_SessionId",
                table: "Assessments",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_AssessmentSessions_SessionId",
                table: "Assessments",
                column: "SessionId",
                principalTable: "AssessmentSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_AssessmentSessions_SessionId",
                table: "Assessments");

            migrationBuilder.DropTable(
                name: "AssessmentSessions");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_SessionId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Assessments");
        }
    }
}
