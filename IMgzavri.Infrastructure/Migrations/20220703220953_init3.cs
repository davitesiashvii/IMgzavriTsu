using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMgzavri.Infrastructure.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExcecute",
                table: "Statements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExcecute",
                table: "Statements",
                type: "bit",
                nullable: true);
        }
    }
}
