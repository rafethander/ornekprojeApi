using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class OdemeEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odeme",
                columns: table => new
                {
                    OdemeId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Durum = table.Column<int>(nullable: false),
                    OdemeTarih = table.Column<DateTime>(nullable: false),
                    FirmaAdi = table.Column<string>(nullable: true),
                    OdenecekTutar = table.Column<double>(nullable: false),
                    Kime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeme", x => x.OdemeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odeme");
        }
    }
}
