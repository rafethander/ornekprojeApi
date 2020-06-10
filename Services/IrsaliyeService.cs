using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Services
{

    public interface IIrsaliyeService
    {
        Task<ApiResult> Add(IrsaliyeAddDto modelIrsaliye);
        Task<ApiResult> Update(IrsaliyeUpdateDto modelIrsaliye);
        Task<ICollection<IrsaliyeGetDto>> Get(IrsaliyeGetModelDto model);
        Task<ApiResult> Delete(int irsaliyeNo);
        Task<ICollection<GetWithIrsaliyeNoDto>> GetWithIrsaliyeNo(int irsaliyeNo);
        Task<ICollection<GetWithMusteriAdiAndIrsaliyeNoDto>> GetWithMusteriAdiAndIrsaliyeNo(IrsaliyeGetModelDto model);

    }
    public class IrsaliyeService : IIrsaliyeService
    {
        private readonly FirinWebApiDbContext _context;
        public IrsaliyeService(FirinWebApiDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult> Add(IrsaliyeAddDto modelIrsaliye)
        {
            //Validation
            if (modelIrsaliye.IrsaliyeNo == 0)
                return new ApiResult { Data = modelIrsaliye.IrsaliyeNo, Message = ApiResultMessages.INE001 };
            if (await _context.Irsaliye.AnyAsync(x =>!x.Silindi && x.IrsaliyeNo == modelIrsaliye.IrsaliyeNo))
                return new ApiResult { Data = modelIrsaliye.IrsaliyeNo, Message = ApiResultMessages.INW001 };


            for (var i=0;i<modelIrsaliye.SatilanUrunId.Count;i++)
            {
                var entitySatilanUrunSatis = new SatilanUrunSatis();
                var entityIrsaliye = new Irsaliye();
                entityIrsaliye = new Irsaliye
                {
                    IrsaliyeId = Guid.NewGuid(),
                    Olusturuldu = DateTime.UtcNow
                };

                entityIrsaliye.IrsaliyeNo = modelIrsaliye.IrsaliyeNo;
                entityIrsaliye.Tarih = modelIrsaliye.Tarih;
               
                //SatilanUrunSatis
                var entitySatilanUrun = await _context.SatilanUrun.Where(x => !x.Silindi && x.SatilanUrunId == modelIrsaliye.SatilanUrunId[i]).FirstOrDefaultAsync();
                entitySatilanUrunSatis.SatilanUrunSatisId = Guid.NewGuid();
                entitySatilanUrunSatis.SatilanUrun = entitySatilanUrun;
                entitySatilanUrunSatis.SatilanUrunId = modelIrsaliye.SatilanUrunId[i];
                entitySatilanUrunSatis.Miktar = modelIrsaliye.Miktar[i];
                entitySatilanUrunSatis.KdvTutar = modelIrsaliye.KdvTutar[i];
                entitySatilanUrunSatis.Tutar =modelIrsaliye.Tutar[i];
                entitySatilanUrunSatis.IrsaliyeNo = modelIrsaliye.IrsaliyeNo;
                entitySatilanUrunSatis.IrsaliyeId = entityIrsaliye.IrsaliyeId;
                entitySatilanUrunSatis.Irsaliye = entityIrsaliye;

                //MusteriIrsaliye
                var entityMusteriIrsaliye = new MusteriIrsaliye
                {
                    MusteriIrsaliyeId = Guid.NewGuid(),
                    Olusturuldu = DateTime.UtcNow
                };

                var entityMusteri = await _context.Musteri.Where(x => !x.Silindi && x.MusteriId == modelIrsaliye.MusteriId).FirstOrDefaultAsync();

                entityMusteriIrsaliye.MusteriId = modelIrsaliye.MusteriId;
                entityMusteriIrsaliye.Musteri = entityMusteri;
                entityMusteriIrsaliye.IrsaliyeId = entityIrsaliye.IrsaliyeId;
                entityMusteriIrsaliye.Irsaliye = entityIrsaliye;


                await _context.MusteriIrsaliye.AddAsync(entityMusteriIrsaliye);
                await _context.SatilanUrunSatis.AddAsync(entitySatilanUrunSatis);
                await _context.Irsaliye.AddAsync(entityIrsaliye);
                await _context.SaveChangesAsync();
            }



            return new ApiResult { Data = modelIrsaliye.IrsaliyeNo, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Update(IrsaliyeUpdateDto modelIrsaliye)
        {
            if (modelIrsaliye.IrsaliyeNo == 0)
                return new ApiResult { Data = modelIrsaliye.IrsaliyeNo, Message = ApiResultMessages.INE001 };


            var entityIrsaliyeler = await _context.Irsaliye
                            .Where(x => !x.Silindi && x.IrsaliyeNo==modelIrsaliye.eskiIrsaliyeNo).ToListAsync();

            var entitySatilanUrunSatisIrsaliyeler= await _context.SatilanUrunSatis
                            .Where(x =>x.IrsaliyeNo == modelIrsaliye.eskiIrsaliyeNo).ToListAsync();

            foreach (var item in entityIrsaliyeler)
            {
                item.IrsaliyeNo = 0;
            }

            foreach (var item in entitySatilanUrunSatisIrsaliyeler)
            {
                item.IrsaliyeNo = 0;
            }

            for (var i = 0; i < modelIrsaliye.SatilanUrunId.Count; i++)
            {
                var entitySatilanUrunSatis = new SatilanUrunSatis();
                var entityIrsaliye = new Irsaliye();
                entityIrsaliye = new Irsaliye
                {
                    IrsaliyeId = Guid.NewGuid(),
                    Olusturuldu = DateTime.UtcNow
                };

                entityIrsaliye.IrsaliyeNo = modelIrsaliye.IrsaliyeNo;
                entityIrsaliye.Tarih = modelIrsaliye.Tarih;

                //SatilanUrunSatis
                var entitySatilanUrun = await _context.SatilanUrun.Where(x => !x.Silindi && x.SatilanUrunId == modelIrsaliye.SatilanUrunId[i]).FirstOrDefaultAsync();
                entitySatilanUrunSatis.SatilanUrunSatisId = Guid.NewGuid();
                entitySatilanUrunSatis.SatilanUrun = entitySatilanUrun;
                entitySatilanUrunSatis.SatilanUrunId = modelIrsaliye.SatilanUrunId[i];
                entitySatilanUrunSatis.Miktar = modelIrsaliye.Miktar[i];
                entitySatilanUrunSatis.KdvTutar = modelIrsaliye.KdvTutar[i];
                entitySatilanUrunSatis.Tutar = modelIrsaliye.Tutar[i];
                entitySatilanUrunSatis.IrsaliyeNo = modelIrsaliye.IrsaliyeNo;
                entitySatilanUrunSatis.IrsaliyeId = entityIrsaliye.IrsaliyeId;
                entitySatilanUrunSatis.Irsaliye = entityIrsaliye;

                //MusteriIrsaliye
                var entityMusteriIrsaliye = new MusteriIrsaliye
                {
                    MusteriIrsaliyeId = Guid.NewGuid(),
                    Olusturuldu = DateTime.UtcNow
                };

                var entityMusteri = await _context.Musteri.Where(x => !x.Silindi && x.MusteriId == modelIrsaliye.MusteriId).FirstOrDefaultAsync();

                entityMusteriIrsaliye.MusteriId = modelIrsaliye.MusteriId;
                entityMusteriIrsaliye.Musteri = entityMusteri;
                entityMusteriIrsaliye.IrsaliyeId = entityIrsaliye.IrsaliyeId;
                entityMusteriIrsaliye.Irsaliye = entityIrsaliye;


                await _context.MusteriIrsaliye.AddAsync(entityMusteriIrsaliye);
                await _context.SatilanUrunSatis.AddAsync(entitySatilanUrunSatis);
                await _context.Irsaliye.AddAsync(entityIrsaliye);
                await _context.SaveChangesAsync();
            }


            var entityIrsaliyeSil = await _context.Irsaliye.Where(x => !x.Silindi && x.IrsaliyeNo == 0).ToListAsync();
           
            var entitySatilanUrunSatisSil = await _context.SatilanUrunSatis.Where(x => x.IrsaliyeNo == 0).ToListAsync();

            _context.Irsaliye.RemoveRange(entityIrsaliyeSil);
            _context.SatilanUrunSatis.RemoveRange(entitySatilanUrunSatisSil);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = modelIrsaliye.IrsaliyeNo, Message = ApiResultMessages.Ok };




        }

        public async Task<ApiResult> Delete(int irsaliyeNo)
        {
            var entity = await _context.Irsaliye.Where(i =>!i.Silindi && i.IrsaliyeNo == irsaliyeNo).ToListAsync();
            if (entity == null)
                return new ApiResult { Data =irsaliyeNo, Message = ApiResultMessages.INW002 };

            foreach (var item in entity)
            {
                item.Silindi = true;
                await _context.SaveChangesAsync();
            }

            return new ApiResult { Data = irsaliyeNo, Message = ApiResultMessages.Ok };
        }

        //Irsaliye Listelemede
        public async Task<ICollection<IrsaliyeGetDto>> Get(IrsaliyeGetModelDto model)
        {
            if(model.BaslangicTarih==default(DateTime)&& model.BitisTarih==default(DateTime)&& model.IrsaliyeNo == default(int) && model.MusteriId == default(Guid))
            {
                model.BaslangicTarih = new DateTime((DateTime.Now.Year - 1), 1, 1);
                model.BitisTarih = DateTime.Now;
            }
            var entity = await (from i in _context.Irsaliye
                         join mI in _context.MusteriIrsaliye on i.IrsaliyeId equals mI.IrsaliyeId
                         join sUs in _context.SatilanUrunSatis on i.IrsaliyeId equals sUs.IrsaliyeId
                         where (i.Silindi==false) && ((mI.Musteri.MusteriId==model.MusteriId && (model.BaslangicTarih<= i.Tarih && i.Tarih <= model.BitisTarih)) || i.IrsaliyeNo==model.IrsaliyeNo)
                         orderby i.Olusturuldu descending
                         select new IrsaliyeGetDto
                         {
                             IrsaliyeNo = i.IrsaliyeNo,
                             Tarih = i.Tarih,
                             TarihString=i.TarihString,
                             MusteriAdi = mI.Musteri.MusteriAdi,
                             UrunAdi = sUs.SatilanUrun.UrunAdi,
                             Miktar = sUs.Miktar,
                             FaturaNo = i.Fatura.FaturaNo
                         }).ToListAsync();

            return entity;
        }

        //Irsaliye Guncellemede
        public async Task<ICollection<GetWithIrsaliyeNoDto>> GetWithIrsaliyeNo(int irsaliyeNo)
        {
            var entity = await (from i in _context.Irsaliye
                                join m in _context.MusteriIrsaliye on i.IrsaliyeId equals m.IrsaliyeId
                                join s in _context.SatilanUrunSatis on i.IrsaliyeId equals s.IrsaliyeId
                                where i.Silindi == false && i.IrsaliyeNo == irsaliyeNo
                                select new GetWithIrsaliyeNoDto
                                {
                                      Tarih=i.Tarih,
                                      MusteriId=m.Musteri.MusteriId,
                                      MusteriAdi=m.Musteri.MusteriAdi,
                                      IrsaliyeNo=i.IrsaliyeNo,
                                      SatilanUrunId=s.SatilanUrunId,
                                      UrunAdi=s.SatilanUrun.UrunAdi,
                                      Miktar=s.Miktar,
                                      Fiyat=s.SatilanUrun.Fiyat,
                                      Kdv=s.SatilanUrun.Kdv,
                                      KdvTutar=s.KdvTutar,
                                      Tutar=s.Tutar
                                }).ToListAsync();

            return entity;
        }

        //Fatura Icın Irsalıye Lıstelemede
        public async Task<ICollection<GetWithMusteriAdiAndIrsaliyeNoDto>> GetWithMusteriAdiAndIrsaliyeNo(IrsaliyeGetModelDto model)
        {
            var entity = await (from i in _context.Irsaliye
                                join m in _context.MusteriIrsaliye on i.IrsaliyeId equals m.IrsaliyeId
                                join s in _context.SatilanUrunSatis on i.IrsaliyeId equals s.IrsaliyeId
                                where (i.Silindi == false && i.Fatura.FaturaId==null) && ((m.MusteriId == model.MusteriId) && (model.BaslangicTarih <= i.Tarih && i.Tarih <= model.BitisTarih))
                                select new GetWithMusteriAdiAndIrsaliyeNoDto
                                {
                                    IrsaliyeId = i.IrsaliyeId,
                                    IrsaliyeNo = i.IrsaliyeNo,
                                    Tarih = i.Tarih,
                                    TarihString = i.TarihString,
                                    UrunAdi = s.SatilanUrun.UrunAdi,
                                    Fiyat = s.SatilanUrun.Fiyat,
                                    Kdv = s.SatilanUrun.Kdv,
                                    Miktar = s.Miktar,
                                    KdvTutar = s.KdvTutar,
                                    Tutar = s.Tutar

                                }).ToListAsync();

            return entity;
        }
    }


}
