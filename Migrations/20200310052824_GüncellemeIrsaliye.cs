using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class GüncellemeIrsaliye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres1",
                table: "Musteri");

            migrationBuilder.DropColumn(
                name: "Adres2",
                table: "Musteri");

            migrationBuilder.DropColumn(
                name: "Adres3",
                table: "Musteri");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adres1",
                table: "Musteri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres2",
                table: "Musteri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres3",
                table: "Musteri",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
