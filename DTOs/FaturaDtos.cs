using FirinWebApi.Database.Models;
using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
	public class FaturaAddDto
	{
        public int FaturaNo { get; set; }
        public DateTime Olusturuldu { get; set; }
        public DurumEnum Durum { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }

        private string tarihString;

        public string TarihString
        {
            get { return tarihString; }
            set { tarihString = Tarih.ToShortDateString(); }
        }




        //Irsaliye Bilgileri
        public IList<Irsaliye> Irsaliyeler { get; set; }

        
    }

    public class FaturaGetDto
    {
        public Guid FaturaId { get; set; }
        public int FaturaNo { get; set; }
        public DurumEnum Durum { get; set; }
        public DateTime Tarih { get; set; }
        public Guid MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public double KdvTutar { get; set; }
        public double Tutar { get; set; }
        public double ToplamTutar { get; set; }

        private string tarihString;

        public string TarihString
        {
            get { return tarihString; }
            set { tarihString = Tarih.ToShortDateString(); }
        }

    }

    public class FaturaGetDtoModel
    {
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public int FaturaNo { get; set; }
        public Guid MusteriId { get; set; }
    }

    public class FaturaUpdateDto:FaturaAddDto
    {
        public int EskiFaturaNo { get; set; }
    }

    public class FaturaGetWithFaturaNoDto:FaturaGetDto
    {
        public Guid IrsaliyeId { get; set; }
        public int IrsaliyeNo { get; set; }
        public string UrunAdi { get; set; }
        public Guid SatilanUrunId { get; set; }
        public double Miktar { get; set; }
        public double Fiyat { get; set; }
        public double Kdv { get; set; }
        public DateTime IrsaliyeTarih { get; set; }
        private string irsaliyeTarihString;

        public string IrsaliyeTarihString
        { 
            get { return irsaliyeTarihString; }
            set { irsaliyeTarihString = IrsaliyeTarih.ToShortDateString(); }
        }

    }
}
