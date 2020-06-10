using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Database.Models
{
    public class Kullanici
    {
        public Guid KullaniciId { get; set; }
        public DateTime Olusturuldu { get; set; }
        public bool Silindi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
