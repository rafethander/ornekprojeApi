using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class SatilanUrun
    {
        public Guid SatilanUrunId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public string UrunAdi { get; set; }
        public string Birim { get; set; }
        public double Fiyat { get; set; }
        public int Kdv { get; set; }

        
    }

    
}
