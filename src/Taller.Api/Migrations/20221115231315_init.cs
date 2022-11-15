using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taller.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Make = table.Column<string>(type: "varchar(50)", nullable: true),
                    Model = table.Column<string>(type: "varchar(50)", nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
