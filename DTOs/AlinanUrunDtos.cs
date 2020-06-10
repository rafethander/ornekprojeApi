using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
    public class AlinanUrunAddDto
    {
        // Fatura No ekle



        [Required, MaxLength(35)]
        [Display(Name ="Ürün Adı")]
        public string UrunAdi { get; set; }
        [Required, MaxLength(50)]
        [Display(Name ="Tedarikçi Adı")]
        public string TedarikciAdi { get; set; }
        [Required, MaxLength(20)]
        public string Birim { get; set; }
        [Required]
        public double Miktar { get; set; }
        private double fiyat;
        [Required]
        public double Fiyat
        {
            get { return fiyat; }
            set { fiyat = Math.Round(value, 2); }
        }
        public int KDV { get; set; }
        

        private DateTime alimTarih;
        [Required]
        [DataType(DataType.Date)]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime AlimTarih
        {
            get { return alimTarih; }
            set { alimTarih = value.ToLocalTime(); }
        }

        private string alimTarihString;

        public string AlimTarihString
        {
            get { return alimTarihString; }
            set { alimTarihString = AlimTarih.ToShortDateString(); }
        }


    }

    public class AlinanUrunGetDto
    {
        public Guid AlinanUrunId { get; set; }
        public string UrunAdi { get; set; }
        public string TedarikciAdi { get; set; }
        public string Birim { get; set; }
        public double Miktar { get; set; }
        public double Fiyat { get; set; }
        public int KDV { get; set; }
        public DateTime AlimTarih { get; set; }
        private string alimTarihString;

        public string AlimTarihString
        {
            get { return alimTarihString; }
            set { alimTarihString = AlimTarih.ToShortDateString(); }
        }

       
    }

   

    public class AlinanUrunUpdateDto
    {
        public Guid AlinanUrunId { get; set; }
        [Required, MaxLength(35)]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        [Required, MaxLength(50)]
        [Display(Name = "Tedarikçi Adı")]
        public string TedarikciAdi { get; set; }
        [Required, MaxLength(20)]
        public string Birim { get; set; }
        [Required]
        public double Miktar { get; set; }
        private double fiyat;
        [Required]
        public double Fiyat
        {
            get { return fiyat; }
            set { fiyat = Math.Round(value, 2); }
        }
        public int KDV { get; set; }
        private DateTime alimTarih;
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime AlimTarih
        {
            get { return alimTarih; }
            set { alimTarih = value.ToLocalTime(); }
        }
    }
}
