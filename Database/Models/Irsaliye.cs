using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Irsaliye
    {
        public Guid IrsaliyeId { get; set; }
        public int IrsaliyeNo { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public DateTime Tarih { get; set; }
        [NotMapped]
        public string TarihString { get; set; }

        //Fk Fatura
        public virtual Fatura Fatura { get; set; }

        //Fk MusteriIrsaliye
        public virtual ICollection<MusteriIrsaliye> MusteriIrsaliyeler { get; set; }

        //Fk SatilanUrunSatis
        public virtual ICollection<SatilanUrunSatis> SatilanUrunSatislar { get; set; }
        public Irsaliye()
        {
            SatilanUrunSatislar = new List<SatilanUrunSatis>();
            MusteriIrsaliyeler = new List<MusteriIrsaliye>();
        }
    }
}
