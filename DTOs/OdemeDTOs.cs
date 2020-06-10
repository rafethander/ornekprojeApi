using FirinWebApi.Database.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.DTOs
{
	public class OdemeAddDto
	{
		private DateTime odemeTarih;
		[Required]
		[DataType(DataType.Date)]
		public DateTime OdemeTarih
		{
			get { return odemeTarih; }
			set { odemeTarih = value.ToLocalTime(); }
		}

		private string odemeTarihString;

		public string OdemeTarihString
		{
			get { return odemeTarihString; }
			set { odemeTarihString = OdemeTarih.ToShortDateString(); }
		}

		[Required,MaxLength(100)]
        public string FirmaAdi { get; set; }
        [Required]
		private double odenecekTutar;
		[Required]
		public double OdenecekTutar
		{
			get { return odenecekTutar; }
			set { odenecekTutar = Math.Round(value,2); }
		}
		[Required]
		public string Kime { get; set; }
		[Required]
		public OdemeTuruEnum OdemeTuru { get; set; }
		
	}
	
	public class OdemeGetDto
	{
		public Guid OdemeId { get; set; }
		public DateTime Olusturuldu { get; set; }
		public DurumEnum Durum { get; set; }
		public OdemeTuruEnum OdemeTuru { get; set; }
		private string odemeTuru;
		public string OdemeTuruString
		{
			get { return odemeTuru; }
			set { odemeTuru = Enum.GetName(typeof(OdemeTuruEnum), OdemeTuru); } 
		}

		private string odemeTarihString;

		public string OdemeTarihString
		{
			get { return odemeTarihString; }
			set { odemeTarihString = OdemeTarih.ToShortDateString(); }
		}

		public DateTime OdemeTarih { get; set; }
		public string FirmaAdi { get; set; }
		public double OdenecekTutar { get; set; }
		public string Kime { get; set; }
	}

	public class OdemeUpdateDto:OdemeAddDto
	{
		public Guid OdemeId { get; set; }
	}
}
