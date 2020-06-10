using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Musteri
    {
        public Guid MusteriId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public string MusteriAdi { get; set; }
        public string VergiDaire { get; set; }
        public string VergiDaireNo { get; set; }
        public string Adres { get; set; }
        public string Aciklama { get; set; }

        public virtual ICollection<Tahsilat> Tahsilatlar { get; set; }
        public virtual ICollection<MusteriIrsaliye> MusteriIrsaliyeler { get; set; }
        public Musteri()
        {
            MusteriIrsaliyeler = new List<MusteriIrsaliye>();
            Tahsilatlar = new List<Tahsilat>();
        }

    }
}
