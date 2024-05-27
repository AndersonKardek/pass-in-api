using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eventtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Attendee_Id",
                table: "Attendees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attendee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_Attendee_Id",
                table: "Attendees",
                column: "Attendee_Id",
                unique: true,
                filter: "[Attendee_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_CheckIn_Attendee_Id",
                table: "Attendees",
                column: "Attendee_Id",
                principalTable: "CheckIn",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_CheckIn_Attendee_Id",
                table: "Attendees");

            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_Attendee_Id",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "Attendee_Id",
                table: "Attendees");
        }
    }
}
