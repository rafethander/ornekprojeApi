using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class OdemeEklendi1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OdemeTuru",
                table: "Odeme",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdemeTuru",
                table: "Odeme");
        }
    }
}
