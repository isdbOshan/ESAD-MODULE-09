using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalEvidence.Migrations.CarInformationDb
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDetails",
                columns: table => new
                {
                    CarDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    IsStock = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDetails", x => x.CarDetailId);
                });

            migrationBuilder.CreateTable(
                name: "PartsDetails",
                columns: table => new
                {
                    PartsDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PartsPrice = table.Column<decimal>(type: "money", nullable: false),
                    CarDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsDetails", x => x.PartsDetailId);
                    table.ForeignKey(
                        name: "FK_PartsDetails_CarDetails_CarDetailId",
                        column: x => x.CarDetailId,
                        principalTable: "CarDetails",
                        principalColumn: "CarDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartsDetails_CarDetailId",
                table: "PartsDetails",
                column: "CarDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartsDetails");

            migrationBuilder.DropTable(
                name: "CarDetails");
        }
    }
}
