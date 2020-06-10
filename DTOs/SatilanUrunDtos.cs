using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
    public class SatilanUrunAddDto
    {
        [Required, StringLength(30)]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        [Required,MaxLength(20)]
        public string Birim { get; set; }
        private double fiyat;
        [Required]
        public double Fiyat
        {
            get { return fiyat; }
            set { fiyat = Math.Round(value, 2); }
        }
        [Required]
        public int Kdv { get; set; }
    }
    public class SatilanUrunGetDto
    {
        public Guid SatilanUrunId { get; set; }
        public string UrunAdi { get; set; }
        public string Birim { get; set; }
        public double Fiyat { get; set; }
        public int Kdv { get; set; }
    }
    public class SatilanUrunUpdateDto
    {
        public Guid SatilanUrunId { get; set; }
        [Required, StringLength(30)]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        [Required, MaxLength(20)]
        public string Birim { get; set; }
        private double fiyat;
        [Required]
        public double Fiyat
        {
            get { return fiyat; }
            set { fiyat = Math.Round(value, 2); }
        }
        [Required]
        public int Kdv { get; set; }
    }
}
