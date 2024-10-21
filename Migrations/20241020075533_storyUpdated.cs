using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplication.Migrations
{
    /// <inheritdoc />
    public partial class storyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CounterStory_Counters_CounterFromID",
                table: "CounterStory");

            migrationBuilder.DropIndex(
                name: "IX_CounterStory_CounterFromID",
                table: "CounterStory");

            migrationBuilder.DropColumn(
                name: "CounterFromID",
                table: "CounterStory");

            migrationBuilder.AddColumn<string>(
                name: "OlderValue",
                table: "CounterStory",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OlderValue",
                table: "CounterStory");

            migrationBuilder.AddColumn<Guid>(
                name: "CounterFromID",
                table: "CounterStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CounterStory_CounterFromID",
                table: "CounterStory",
                column: "CounterFromID");

            migrationBuilder.AddForeignKey(
                name: "FK_CounterStory_Counters_CounterFromID",
                table: "CounterStory",
                column: "CounterFromID",
                principalTable: "Counters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
