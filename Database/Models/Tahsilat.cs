using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Tahsilat
    {
        public Guid TahsilatId { get; set; }
        public bool Silindi { get; set; }
        public DateTime Olusturuldu { get; set; }
        public DateTime TahsilatTarihi { get; set; }
        [NotMapped]
        public string TahsilatTarihiString { get; set; }
        [NotMapped]
        public string TahsilatTuruString { get; set; }
        public double TahsilatTutar { get; set; }
        public OdemeTuruEnum TahsilatTuru { get; set; }
       


        //Musteri
        public virtual Guid MusteriId { get; set; }
        public virtual Musteri Musteri { get; set; }
    }
}
