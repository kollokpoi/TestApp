using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplication.Migrations
{
    /// <inheritdoc />
    public partial class storyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CounterId",
                table: "Apartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CounterStory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    CounterFromID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CounterToID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterStory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CounterStory_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterStory_Counters_CounterFromID",
                        column: x => x.CounterFromID,
                        principalTable: "Counters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterStory_Counters_CounterToID",
                        column: x => x.CounterToID,
                        principalTable: "Counters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CounterStory_ApartmentId",
                table: "CounterStory",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterStory_CounterFromID",
                table: "CounterStory",
                column: "CounterFromID");

            migrationBuilder.CreateIndex(
                name: "IX_CounterStory_CounterToID",
                table: "CounterStory",
                column: "CounterToID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterStory");

            migrationBuilder.AlterColumn<int>(
                name: "CounterId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
