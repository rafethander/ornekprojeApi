﻿// <auto-generated />
using System;
using FirinWebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirinWebApi.Migrations
{
    [DbContext(typeof(FirinWebApiDbContext))]
    [Migration("20200310052957_GüncellemeIrsaliye1")]
    partial class GüncellemeIrsaliye1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FirinWebApi.Database.Models.AlinanUrun", b =>
                {
                    b.Property<Guid>("AlinanUrunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AlimTarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Birim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Fiyat")
                        .HasColumnType("float");

                    b.Property<int>("KDV")
                        .HasColumnType("int");

                    b.Property<double>("Miktar")
                        .HasColumnType("float");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<string>("TedarikciAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrunAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlinanUrunId");

                    b.ToTable("AlinanUrun");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Fatura", b =>
                {
                    b.Property<Guid>("FaturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<int>("FaturaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<double>("ToplamTutar")
                        .HasColumnType("float");

                    b.HasKey("FaturaId");

                    b.ToTable("Fatura");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Irsaliye", b =>
                {
                    b.Property<Guid>("IrsaliyeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FaturaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IrsaliyeNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("IrsaliyeId");

                    b.HasIndex("FaturaId");

                    b.ToTable("Irsaliye");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Kullanici", b =>
                {
                    b.Property<Guid>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KullaniciAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sifre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanici");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Musteri", b =>
                {
                    b.Property<Guid>("MusteriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<string>("VergiDaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VergiDaireNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MusteriId");

                    b.ToTable("Musteri");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.MusteriIrsaliye", b =>
                {
                    b.Property<Guid>("MusteriIrsaliyeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IrsaliyeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MusteriId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.HasKey("MusteriIrsaliyeId");

                    b.HasIndex("IrsaliyeId");

                    b.HasIndex("MusteriId");

                    b.ToTable("MusteriIrsaliye");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Odeme", b =>
                {
                    b.Property<Guid>("OdemeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("FirmaAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OdemeTarih")
                        .HasColumnType("datetime2");

                    b.Property<int>("OdemeTuru")
                        .HasColumnType("int");

                    b.Property<double>("OdenecekTutar")
                        .HasColumnType("float");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.HasKey("OdemeId");

                    b.ToTable("Odeme");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.SatilanUrun", b =>
                {
                    b.Property<Guid>("SatilanUrunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Birim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Fiyat")
                        .HasColumnType("float");

                    b.Property<int>("Kdv")
                        .HasColumnType("int");

                    b.Property<DateTime>("Olusturuldu")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Silindi")
                        .HasColumnType("bit");

                    b.Property<string>("UrunAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SatilanUrunId");

                    b.ToTable("SatilanUrun");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.SatilanUrunSatis", b =>
                {
                    b.Property<Guid>("SatilanUrunSatisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IrsaliyeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IrsaliyeNo")
                        .HasColumnType("int");

                    b.Property<double>("KdvTutar")
                        .HasColumnType("float");

                    b.Property<double>("Miktar")
                        .HasColumnType("float");

                    b.Property<Guid>("SatilanUrunId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Tutar")
                        .HasColumnType("float");

                    b.HasKey("SatilanUrunSatisId");

                    b.HasIndex("IrsaliyeId");

                    b.HasIndex("SatilanUrunId");

                    b.ToTable("SatilanUrunSatis");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.Irsaliye", b =>
                {
                    b.HasOne("FirinWebApi.Database.Models.Fatura", "Fatura")
                        .WithMany("Irsaliyes")
                        .HasForeignKey("FaturaId");
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.MusteriIrsaliye", b =>
                {
                    b.HasOne("FirinWebApi.Database.Models.Irsaliye", "Irsaliye")
                        .WithMany("MusteriIrsaliyeler")
                        .HasForeignKey("IrsaliyeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FirinWebApi.Database.Models.Musteri", "Musteri")
                        .WithMany("MusteriIrsaliyeler")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FirinWebApi.Database.Models.SatilanUrunSatis", b =>
                {
                    b.HasOne("FirinWebApi.Database.Models.Irsaliye", "Irsaliye")
                        .WithMany("SatilanUrunSatislar")
                        .HasForeignKey("IrsaliyeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FirinWebApi.Database.Models.SatilanUrun", "SatilanUrun")
                        .WithMany()
                        .HasForeignKey("SatilanUrunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
