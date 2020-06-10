using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlinanUrun",
                columns: table => new
                {
                    AlinanUrunId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    AlimTarih = table.Column<DateTime>(nullable: false),
                    UrunAdi = table.Column<string>(nullable: true),
                    TedarikciAdi = table.Column<string>(nullable: true),
                    Miktar = table.Column<double>(nullable: false),
                    Birim = table.Column<string>(nullable: true),
                    Fiyat = table.Column<double>(nullable: false),
                    KDV = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlinanUrun", x => x.AlinanUrunId);
                });

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    FaturaId = table.Column<Guid>(nullable: false),
                    FaturaNo = table.Column<int>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Durum = table.Column<int>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    ToplamTutar = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.FaturaId);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    KullaniciId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    KullaniciAdi = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    MusteriId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    MusteriAdi = table.Column<string>(nullable: true),
                    VergiDaire = table.Column<string>(nullable: true),
                    VergiDaireNo = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    Adres1 = table.Column<string>(nullable: true),
                    Adres2 = table.Column<string>(nullable: true),
                    Adres3 = table.Column<string>(nullable: true),
                    Aciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "SatilanUrun",
                columns: table => new
                {
                    SatilanUrunId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    UrunAdi = table.Column<string>(nullable: true),
                    Birim = table.Column<string>(nullable: true),
                    Fiyat = table.Column<double>(nullable: false),
                    Kdv = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatilanUrun", x => x.SatilanUrunId);
                });

            migrationBuilder.CreateTable(
                name: "Irsaliye",
                columns: table => new
                {
                    IrsaliyeId = table.Column<Guid>(nullable: false),
                    IrsaliyeNo = table.Column<int>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false),
                    FaturaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irsaliye", x => x.IrsaliyeId);
                    table.ForeignKey(
                        name: "FK_Irsaliye_Fatura_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Fatura",
                        principalColumn: "FaturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusteriIrsaliye",
                columns: table => new
                {
                    MusteriIrsaliyeId = table.Column<Guid>(nullable: false),
                    Olusturuldu = table.Column<DateTime>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    IrsaliyeId = table.Column<Guid>(nullable: false),
                    MusteriId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriIrsaliye", x => x.MusteriIrsaliyeId);
                    table.ForeignKey(
                        name: "FK_MusteriIrsaliye_Irsaliye_IrsaliyeId",
                        column: x => x.IrsaliyeId,
                        principalTable: "Irsaliye",
                        principalColumn: "IrsaliyeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriIrsaliye_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatilanUrunSatis",
                columns: table => new
                {
                    SatilanUrunSatisId = table.Column<Guid>(nullable: false),
                    Miktar = table.Column<double>(nullable: false),
                    Tutar = table.Column<double>(nullable: false),
                    IrsaliyeId = table.Column<Guid>(nullable: false),
                    IrsaliyeNo = table.Column<int>(nullable: false),
                    SatilanUrunId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatilanUrunSatis", x => x.SatilanUrunSatisId);
                    table.ForeignKey(
                        name: "FK_SatilanUrunSatis_Irsaliye_IrsaliyeId",
                        column: x => x.IrsaliyeId,
                        principalTable: "Irsaliye",
                        principalColumn: "IrsaliyeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SatilanUrunSatis_SatilanUrun_SatilanUrunId",
                        column: x => x.SatilanUrunId,
                        principalTable: "SatilanUrun",
                        principalColumn: "SatilanUrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irsaliye_FaturaId",
                table: "Irsaliye",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIrsaliye_IrsaliyeId",
                table: "MusteriIrsaliye",
                column: "IrsaliyeId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIrsaliye_MusteriId",
                table: "MusteriIrsaliye",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_SatilanUrunSatis_IrsaliyeId",
                table: "SatilanUrunSatis",
                column: "IrsaliyeId");

            migrationBuilder.CreateIndex(
                name: "IX_SatilanUrunSatis_SatilanUrunId",
                table: "SatilanUrunSatis",
                column: "SatilanUrunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlinanUrun");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "MusteriIrsaliye");

            migrationBuilder.DropTable(
                name: "SatilanUrunSatis");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "Irsaliye");

            migrationBuilder.DropTable(
                name: "SatilanUrun");

            migrationBuilder.DropTable(
                name: "Fatura");
        }
    }
}
