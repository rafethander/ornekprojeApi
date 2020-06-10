using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
    public class KullaniciAuthenticateDto
    {
        [Required]
        public string KullaniciAdi { get; set; }

        [Required]
        public string Sifre { get; set; }

    }
    public class KullaniciAddDto
    {
        [Required,MaxLength(40)]
        public string Ad { get; set; }

        [Required,MaxLength(40)]
        public string Soyad { get; set; }

        [Required,MaxLength(40)]
        public string KullaniciAdi { get; set; }

        [Required,MaxLength(20)]
        public string Sifre { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class KullaniciUpdateDto:KullaniciAddDto
    {
        [Required]
        public Guid KullaniciId { get; set; }
        
    }

    public class KullaniciGetDto
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Role { get; set; }
    }
}
