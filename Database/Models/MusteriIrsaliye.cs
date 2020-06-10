using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class MusteriIrsaliye
    {
        public Guid MusteriIrsaliyeId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        

        public Guid IrsaliyeId { get; set; }
        public virtual Irsaliye Irsaliye { get; set; }

        public Guid MusteriId { get; set; }
        public virtual Musteri Musteri { get; set; }

    }
}
