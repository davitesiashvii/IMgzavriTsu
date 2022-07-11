using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMgzavri.Infrastructure.Migrations
{
    public partial class init20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTechnicalInspection",
                table: "CarImages",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsTechnicalInspection",
                table: "CarImages");
        }
    }
}
