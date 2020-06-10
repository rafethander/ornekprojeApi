using FirinWebApi.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
	public class MusteriAddDto
	{
        [MaxLength(35), Required]
        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }
        [MaxLength(150), Required]
        [Display(Name = "Adres Bilgisi")]
        public string Adres { get; set; }
        [MaxLength(35), Required]
        [Display(Name = "Vergi Dairesi")]
        public string VergiDaire { get; set; }
        [MaxLength(35), Required]
        [Display(Name = "Vergi Numarası")]
        public string VergiDaireNo { get; set; }
        [MaxLength(100)]
        public string Aciklama { get; set; }

    }
    public class MusteriGetDto
    {
        public Guid MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public string  VergiDaireNo { get; set; }
        public string VergiDaire { get; set; }
        public string Adres { get; set; }
        public string Aciklama { get; set; }
    }
    
    public class MusteriUpdateDto
    {
        [Required]
        public Guid MusteriId { get; set; }
        [MaxLength(35),Required]
        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }
        [MaxLength(35),Required]
        [Display(Name = "Adres Bilgisi")]
        public string Adres { get; set; }
        [MaxLength(35),Required]
        [Display(Name = "Vergi Dairesi")]
        public string VergiDaire { get; set; }
        [MaxLength(35),Required]
        [Display(Name = "Vergi Numarası")]
        public string VergiDaireNo { get; set; }
        public string Aciklama { get; set; }
       
    }

    public class MusteriSelectDto
    {
        public Guid MusteriId { get; set; }
        public string MusteriAdi { get; set; }
    }

}
