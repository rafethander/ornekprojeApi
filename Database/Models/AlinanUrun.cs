using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class AlinanUrun
    {
        public Guid AlinanUrunId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public DateTime AlimTarih { get; set; }
        public string UrunAdi { get; set; }
        public string TedarikciAdi { get; set; }
        public double Miktar { get; set; }
        public string Birim { get; set; }
        public double Fiyat { get; set; }
        public int KDV { get; set; }

        [NotMapped]
        public string AlimTarihString { get; set; }

    }
}
