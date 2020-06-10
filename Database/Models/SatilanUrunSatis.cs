using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class SatilanUrunSatis
    {
        public Guid SatilanUrunSatisId { get; set; }
        public double Miktar { get; set; }
        public double KdvTutar { get; set; }
        public double Tutar { get; set; }
       


        //Fk Irsaliye
        public virtual Irsaliye Irsaliye { get; set; }
        public Guid IrsaliyeId { get; set; }
        public int IrsaliyeNo { get; set; }

        //Fk SatilanUrun
        public Guid SatilanUrunId { get; set; }
        public virtual SatilanUrun SatilanUrun { get; set; }



    }
}
