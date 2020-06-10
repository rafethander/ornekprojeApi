using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Fatura
    {
        public Guid FaturaId { get; set; }
        public int FaturaNo { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public DurumEnum Durum { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        [NotMapped]
        public string TarihString { get; set; }


        //Fk
        //Irsaliye
        public virtual ICollection<Irsaliye> Irsaliyes { get; set; }
        public Fatura()
        {
            Irsaliyes = new List<Irsaliye>();
        }
    }
}
