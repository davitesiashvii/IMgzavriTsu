using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMgzavri.Infrastructure.Migrations
{
    public partial class init21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Cars");

            migrationBuilder.AddColumn<bool>(
                name: "IsVertify",
                table: "Cars",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVertify",
                table: "Cars");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
