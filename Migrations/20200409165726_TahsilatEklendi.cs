using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class TahsilatEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tahsilat",
                columns: table => new
                {
                    TahsilatId = table.Column<Guid>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    TahsilatTarihi = table.Column<DateTime>(nullable: false),
                    TahsilatTuru = table.Column<int>(nullable: false),
                    TahsilatTutar = table.Column<double>(nullable: false),
                    MusteriId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tahsilat", x => x.TahsilatId);
                    table.ForeignKey(
                        name: "FK_Tahsilat_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tahsilat_MusteriId",
                table: "Tahsilat",
                column: "MusteriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tahsilat");
        }
    }
}
