using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirinWebApi.Database.Models.Enum;

namespace FirinWebApi.Services
{
    public interface IFaturaService
    {
        Task<ApiResult> Add(FaturaAddDto model);
        Task<IEnumerable<FaturaGetDto>> Get(FaturaGetDtoModel model);
        Task<ApiResult> Delete(int faturaNo);
        Task<ICollection<FaturaGetWithFaturaNoDto>> GetWithFaturaNo(int faturaNo); 
        Task<ApiResult> Update(FaturaUpdateDto model);
    }
    public class FaturaService : IFaturaService
    {
        private readonly FirinWebApiDbContext _context;
        public FaturaService(FirinWebApiDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult> Add(FaturaAddDto model)
        {
            //Validation

            if (model.FaturaNo == 0)
                return new ApiResult { Data = model.FaturaNo, Message = ApiResultMessages.INE001 };

            if (await _context.Fatura.AnyAsync(x => !x.Silindi && x.FaturaNo == model.FaturaNo))
                return new ApiResult { Data = model.FaturaNo, Message = ApiResultMessages.FNW001 };



            var entityFatura = new Fatura()
            {
                FaturaId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
                Durum = DurumEnum.KayıtEdildi
            };
            entityFatura.FaturaNo = model.FaturaNo;
            entityFatura.Tarih = model.Tarih;
            entityFatura.ToplamTutar = model.ToplamTutar;

            await _context.Fatura.AddAsync(entityFatura);


            var IrsaliyeListe = new List<Irsaliye>();
            foreach (var irsaliye in model.Irsaliyeler)
            {
                IrsaliyeListe.Add(irsaliye);
            }
            foreach (var irsaliye in IrsaliyeListe)
            {
                var entityIrsaliye = await _context.Irsaliye.Where(x => !x.Silindi && x.IrsaliyeId == irsaliye.IrsaliyeId).FirstOrDefaultAsync();


                entityIrsaliye.Fatura = entityFatura;
                // _context.Entry<Irsaliye>(entityIrsaliye).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }




            return new ApiResult { Data = entityFatura.FaturaNo, Message = ApiResultMessages.Ok };
        }

        //Fatura Listelemede
        public async Task<IEnumerable<FaturaGetDto>> Get(FaturaGetDtoModel model)
        {
            if (model.BaslangicTarih == default(DateTime) && model.BitisTarih == default(DateTime) && model.FaturaNo == default(int) && model.MusteriId == default(Guid))
            {
                model.BaslangicTarih = new DateTime((DateTime.Now.Year - 1), 1, 1);
                model.BitisTarih = DateTime.Now;
            }

            var entity = await (from f in _context.Fatura
                                join i in _context.Irsaliye on f.FaturaId equals i.Fatura.FaturaId
                                join m in _context.MusteriIrsaliye on i.IrsaliyeId equals m.IrsaliyeId
                                join s in _context.SatilanUrunSatis on i.IrsaliyeId equals s.IrsaliyeId
                                where (f.Silindi == false) && ((m.MusteriId == model.MusteriId && (model.BaslangicTarih <= f.Tarih && model.BitisTarih >= f.Tarih)) || (f.FaturaNo == model.FaturaNo))
                                orderby f.Olusturuldu descending
                                select new FaturaGetDto
                                {
                                    FaturaId = f.FaturaId,
                                    FaturaNo = f.FaturaNo,
                                    Durum = DurumEnum.KayıtEdildi,
                                    Tarih = f.Tarih,
                                    ToplamTutar = f.ToplamTutar,
                                    TarihString = f.TarihString,
                                    MusteriId = m.MusteriId,
                                    MusteriAdi = m.Musteri.MusteriAdi,
                                    KdvTutar = s.KdvTutar,
                                    Tutar = s.Tutar
                                }).ToListAsync();

            double KdvTutar = 0;
            double Tutar = 0;
            var listeFatura = new List<FaturaGetDto>();
            foreach (var item in entity)
            {
                foreach (var fatura in entity)
                {
                    if (item.FaturaId == fatura.FaturaId)
                    {
                        KdvTutar = KdvTutar + fatura.KdvTutar;
                        Tutar = Tutar + fatura.Tutar;

                    }

                }
                var yeniFatura = new FaturaGetDto
                {
                    FaturaId = item.FaturaId,
                    Durum = DurumEnum.KayıtEdildi,
                    FaturaNo = item.FaturaNo,
                    Tarih = item.Tarih,
                    TarihString = item.TarihString,
                    ToplamTutar = item.ToplamTutar,
                    MusteriId = item.MusteriId,
                    MusteriAdi = item.MusteriAdi,
                    KdvTutar = KdvTutar,
                    Tutar = Tutar
                };
                listeFatura.Add(yeniFatura);
                KdvTutar = 0;
                Tutar = 0;
            }

            var FaturaListe = listeFatura.GroupBy(f => f.FaturaNo).Select(f => f.First());



            return FaturaListe;




        }

        public async Task<ApiResult> Delete(int faturaNo)
        {
            var entity = await _context.Fatura.Where(x => !x.Silindi && x.FaturaNo == faturaNo).FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = faturaNo, Message = ApiResultMessages.FNW002 };

            entity.Silindi = true;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.FaturaNo, Message = ApiResultMessages.Ok };
        }



        public async Task<ApiResult> Update(FaturaUpdateDto model)
        {
            if (model.FaturaNo == 0)
                return new ApiResult { Data = model.FaturaNo, Message = ApiResultMessages.INE001 };

            var entityFaturaEski = await _context.Fatura.Where(x => !x.Silindi && x.FaturaNo == model.EskiFaturaNo).FirstOrDefaultAsync();

            if (entityFaturaEski == null)
                return new ApiResult { Data = model.FaturaNo, Message = ApiResultMessages.FNW002 };

            entityFaturaEski.FaturaNo = 0;

            var entityIrsaliyeEski = await _context.Irsaliye.Where(x => !x.Silindi && x.Fatura.FaturaId == entityFaturaEski.FaturaId).ToListAsync();

            foreach (var item in entityIrsaliyeEski)
            {
                item.Fatura.FaturaNo = 0;
            }


            var entityFatura = new Fatura()
            {
                FaturaId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
                Durum = DurumEnum.KayıtEdildi
            };
            entityFatura.FaturaNo = model.FaturaNo;
            entityFatura.Tarih = model.Tarih;
            entityFatura.ToplamTutar = model.ToplamTutar;

            await _context.Fatura.AddAsync(entityFatura);


            var IrsaliyeListe = new List<Irsaliye>();
            foreach (var irsaliye in model.Irsaliyeler)
            {
                IrsaliyeListe.Add(irsaliye);
            }
            foreach (var irsaliye in IrsaliyeListe)
            {
                var entityIrsaliye = await _context.Irsaliye.Where(x => !x.Silindi && x.IrsaliyeId == irsaliye.IrsaliyeId).FirstOrDefaultAsync();


                entityIrsaliye.Fatura = entityFatura;
                // _context.Entry<Irsaliye>(entityIrsaliye).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var entityFaturaSil = await _context.Fatura.Where(x => !x.Silindi && x.FaturaNo == 0).FirstOrDefaultAsync();
            var entityIrsaliyeSil = await _context.Irsaliye.Where(x => !x.Silindi && x.Fatura.FaturaNo == 0).ToListAsync();

            _context.Fatura.Remove(entityFaturaSil);
            _context.Irsaliye.RemoveRange(entityIrsaliyeSil);
            await _context.SaveChangesAsync();



            return new ApiResult { Data = entityFatura.FaturaNo, Message = ApiResultMessages.Ok };


        }

        public async Task<ICollection<FaturaGetWithFaturaNoDto>> GetWithFaturaNo(int faturaNo)
        {
            var entity = await (from f in _context.Fatura
                                join i in _context.Irsaliye on f.FaturaId equals i.Fatura.FaturaId
                                join Mi in _context.MusteriIrsaliye on i.IrsaliyeId equals Mi.IrsaliyeId
                                join Su in _context.SatilanUrunSatis on i.IrsaliyeId equals Su.IrsaliyeId
                                where (f.Silindi == false && f.FaturaNo == faturaNo && i.Fatura.FaturaNo==faturaNo)
                                select new FaturaGetWithFaturaNoDto
                                {
                                    FaturaId=f.FaturaId,
                                    FaturaNo=f.FaturaNo,
                                    Durum=f.Durum,
                                    Tarih=f.Tarih,
                                    TarihString = f.TarihString,
                                    ToplamTutar =f.ToplamTutar,

                                    MusteriId=Mi.Musteri.MusteriId,
                                    MusteriAdi=Mi.Musteri.MusteriAdi,

                                    IrsaliyeId=i.IrsaliyeId,
                                    IrsaliyeNo=i.IrsaliyeNo,
                                    IrsaliyeTarih=i.Tarih,
                                    IrsaliyeTarihString=i.TarihString,
                                    UrunAdi=Su.SatilanUrun.UrunAdi,
                                    Fiyat=Su.SatilanUrun.Fiyat,
                                    Kdv=Su.SatilanUrun.Kdv,
                                    Miktar=Su.Miktar,
                                    KdvTutar=Su.KdvTutar,
                                    Tutar=Su.Tutar,
                                   
                                    
                                }).ToListAsync();

            return entity;
        }
    }
}
