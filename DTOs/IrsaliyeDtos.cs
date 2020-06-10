using FirinWebApi.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
    public class IrsaliyeAddDto
    {
        [Required]
        // [Display(Name = "İrsaliye Numarası")]
        public int IrsaliyeNo { get; set; }

        private DateTime tarih;
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value.ToLocalTime(); }
        }

        private string tarihString;

        public string TarihString
        {
            get { return tarihString; }
            set { tarihString = Tarih.ToShortDateString(); }
        }



        //Musteri

        public Guid MusteriId { get; set; }

        //SatilanUrunSatis
        public IList<Guid> SatilanUrunId { get; set; }
        public IList<SatilanUrun> SatilanUrun { get; set; }
        public IList<double> Miktar { get; set; }
        public IList<double> KdvTutar { get; set; }
        public IList<double> Tutar { get; set; }

    }


    public class IrsaliyeGetDto
    {
        public int IrsaliyeNo { get; set; }
        public string MusteriAdi { get; set; }
        public string UrunAdi { get; set; }
        public double Miktar { get; set; }
        public int FaturaNo { get; set; }
        public DateTime Tarih { get; set; }
        private string tarihString;
        public string TarihString
        {
            get { return tarihString; }
            set { tarihString = Tarih.ToShortDateString(); }
        }
    }
    public class IrsaliyeGetModelDto
    {
        public int? IrsaliyeNo { get; set; }
        public Guid MusteriId{ get; set; }
        private DateTime baslangictarih;
        [Required]
        public DateTime BaslangicTarih
        {
            get { return baslangictarih; }
            set { baslangictarih = value.ToLocalTime(); }
        }

        private DateTime bitistarih;
        [Required]
        public DateTime BitisTarih
        {
            get { return bitistarih; }
            set { bitistarih = value.ToLocalTime(); }
        }
    }

    public class GetWithIrsaliyeNoDto
    {
        public int IrsaliyeNo { get; set; }
        public Guid MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public string UrunAdi { get; set; }
        public Guid SatilanUrunId { get; set; }
        public double Miktar { get; set; }
        public double Fiyat { get; set; }
        public double Kdv { get; set; }
        public double KdvTutar { get; set; }
        public double Tutar { get; set; }
        
        public DateTime Tarih { get; set; }
       
    }

    public class IrsaliyeUpdateDto:IrsaliyeAddDto
    {
        [Required]
        public int eskiIrsaliyeNo { get; set; }

    }

    public class GetWithMusteriAdiAndIrsaliyeNoDto:GetWithIrsaliyeNoDto
    {
        [Required]
        public Guid IrsaliyeId { get; set; }
        private string tarihString;

        public string TarihString
        {
            get { return tarihString; }
            set { tarihString = Tarih.ToShortDateString(); }
        }

    }
}
