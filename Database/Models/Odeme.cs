using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Odeme
    {
        public Guid OdemeId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public DurumEnum Durum { get; set; }
        public OdemeTuruEnum OdemeTuru { get; set; }
        public DateTime OdemeTarih { get; set; }
        public string FirmaAdi { get; set; }
        public double OdenecekTutar { get; set; }
        public string Kime { get; set; }

        [NotMapped]
        public string OdemeTuruString { get; set; }
        [NotMapped]
        public string OdemeTarihString { get; set; }


    }
}
