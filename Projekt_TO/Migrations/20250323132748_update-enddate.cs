using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Projekt_TO.Migrations
{
    public partial class updateenddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Rentals",
                type: "DATETIME",
                nullable: false,
                computedColumnSql: "DATEADD(DAY, Days, StartDate)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Rentals");
        }
    }
}
