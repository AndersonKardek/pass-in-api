using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationshipkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Attendees");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_Event_Id",
                table: "Attendees",
                column: "Event_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_Event_Id",
                table: "Attendees",
                column: "Event_Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_Event_Id",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_Event_Id",
                table: "Attendees");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Attendees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
