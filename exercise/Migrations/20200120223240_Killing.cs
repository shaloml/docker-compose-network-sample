using Microsoft.EntityFrameworkCore.Migrations;

namespace ex4.Migrations
{
    public partial class Killing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Killing",
                table: "Samurais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Killing",
                table: "Samurais");
        }
    }
}
