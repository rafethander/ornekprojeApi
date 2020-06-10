using FirinWebApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database
{
    public class FirinWebApiDbContext:DbContext
    {
        public FirinWebApiDbContext(DbContextOptions<FirinWebApiDbContext> options):base(options)
        {

        }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<AlinanUrun> AlinanUrun { get; set; }
        public DbSet<SatilanUrun> SatilanUrun { get; set; }
        public DbSet<SatilanUrunSatis> SatilanUrunSatis { get; set; }
        public DbSet<Irsaliye> Irsaliye { get; set; }
        public DbSet<Fatura> Fatura { get; set; }
        public DbSet<Tahsilat> Tahsilat { get; set; }
        public DbSet<MusteriIrsaliye> MusteriIrsaliye { get; set; }
        public DbSet<Odeme> Odeme { get; set; }



    }
}
