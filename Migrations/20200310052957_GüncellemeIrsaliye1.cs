using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class GüncellemeIrsaliye1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "KdvTutar",
                table: "SatilanUrunSatis",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KdvTutar",
                table: "SatilanUrunSatis");
        }
    }
}
