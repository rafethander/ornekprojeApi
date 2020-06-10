using FirinWebApi.Database.Models;
using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
    public class TahsilatAddDto
    {
        [Required]
        public DateTime TahsilatTarihi { get; set; }
        [Required]
        public OdemeTuruEnum TahsilatTuru { get; set; }

        private double tahsilatTutar;

        public double TahsilatTutar
        {
            get { return tahsilatTutar; }
            set { tahsilatTutar = Math.Round(value,2); }
        }



        public  Guid MusteriId { get; set; }
        public Musteri Musteri { get; set; }
    }

    public class TahsilatGetDto
    {
        public Guid TahsilatId { get; set; }
        public bool Silindi { get; set; }
        public DateTime Olusturuldu { get; set; }
        public DateTime TahsilatTarihi { get; set; }
        private string tahsilatTarihiString;

        public string TahsilatTarihiString
        {
            get { return tahsilatTarihiString; }
            set { tahsilatTarihiString = TahsilatTarihi.ToShortDateString(); }
        }
        
        public double TahsilatTutar { get; set; }
        public OdemeTuruEnum TahsilatTuru { get; set; }
        private string tahsilatTuruString;
        public string TahsilatTuruString
        {
            get { return tahsilatTuruString; }
            set { tahsilatTuruString = Enum.GetName(typeof(OdemeTuruEnum), TahsilatTuru); }
        }



        //Musteri
        public  Guid MusteriId { get; set; }
        public  string  MusteriAdi { get; set; }
    }

    public class FaturaToplamTutar
    {
        public int  FaturaNo { get; set; }
        public double ToplamTutar { get; set; }
    }
}
