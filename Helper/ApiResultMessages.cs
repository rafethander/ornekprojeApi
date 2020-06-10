using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Helper
{
    /// <summary>
    /// Hata Kodu 6 Karakter Olucak
    /// ilk 2 karakter ekranı
    /// 3. karakter Hatayı (I:Info,W=Warning,E=Error..)
    /// Son 3 karakter Hata Kodunu Belirtir.
    /// </summary>
    public class ApiResultMessages
    {
        /// <summary>
        /// Başarılı
        /// </summary>
        public const string Ok = "Ok";
        /// <summary>
        /// MUsteriError
        /// Müşteri Bulunamadı.
        /// </summary>
        public const string MUE001 = "MUE001";
        /// <summary>
        /// MusteriAdiWarning
        /// MusteriAdi Sistemde var.
        /// </summary>
        public const string MUW001 = "MUW001";
        /// <summary>
        /// AlınanUrunError
        /// Alınan Urun Bulunamadı.
        /// </summary>
        public const string AUE001 = "AUE001";
        /// <summary>
        /// SatılanUrunError
        /// Satilan Urun Bulunamadı.
        /// </summary>
        public const string SUE001 = "SUE001";
        /// <summary>
        /// KullaniciAdıWarning
        /// KullaniciAdi Sistemde var.
        /// </summary>
        public const string KAW001 = "KAW001";
        /// <summary>
        /// KUllaniciError
        /// Kullanici Sistemde bulunamadı.
        /// </summary>
        public const string KUE001 = "KUE001";
        /// <summary>
        /// KullaniciSifreError
        /// Kullanici Sifresi Hatalı.
        /// </summary>
        public const string KSE001 = "KUS001";
        /// <summary>
        /// IrsaliyeNumarasıWarning
        /// Irsaliye Numarası Sistemde var.
        /// </summary>
        public const string INW001 = "INW001";
        /// <summary>
        /// FaturaNumarasıWarning
        /// Fatura Numarası Sistemde var.
        /// </summary>
        public const string FNW001 = "FNW001";
        /// <summary>
        /// ODemeError
        /// Odeme Sistemde Bulunamadı
        /// </summary>
        public const string ODE001 = "ODE001";
        /// <summary>
        /// İrsaliyeNumaraError
        /// İrsaliye Numarası Sistemde Bulunamadı.
        /// </summary>
        public const string INW002 = "INE002";
        /// <summary>
        /// İrsaliyeNumarasıError
        /// İrsaliye Numarası 0(sıfır) Olamaz.
        /// </summary>
        public const string INE001 = "INE001";
        /// <summary>
        /// FaturaNumarasıWarning
        /// Fatura Numarası Sistemde Bulunamadı.
        /// </summary>
        public const string FNW002 = "FNW002";
        /// <summary>
        /// FaturaNumarasıError
        /// Fatura Numarası 0(sıfır) Olamaz.
        /// </summary>
        public const string FNE001 = "FNE001";
        /// <summary>
        /// TAhsilatBulunamadı
        /// Tahsilat Bulunamadı.
        /// </summary>
        public const string TAW001 = "TAW001";


    }
}
