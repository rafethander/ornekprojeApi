using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirinWebApi.Migrations
{
    public partial class KullaniciGuncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sifre",
                table: "Kullanici");

            migrationBuilder.AddColumn<byte[]>(
                name: "SifreHash",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SifreSalt",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Kullanici",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SifreHash",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "SifreSalt",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Kullanici");

            migrationBuilder.AddColumn<string>(
                name: "Sifre",
                table: "Kullanici",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
