using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirinWebApi.Database.Models.Enum;
using FirinWebApi.Database;
using Microsoft.EntityFrameworkCore;

namespace FirinWebApi.Services
{
    public interface IOdemeService
    {
        Task<ApiResult> Add(OdemeAddDto model);
        Task<ApiResult> Update(OdemeUpdateDto model);
        Task<ApiResult> Delete(Guid id);
        Task<ApiResult> Odendi(Guid id);
        Task<ICollection<OdemeGetDto>> Get();
    }
    public class OdemeService : IOdemeService
    {
        private readonly FirinWebApiDbContext _context;
        public OdemeService(FirinWebApiDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult> Add(OdemeAddDto model)
        {
            var entity = new Odeme
            {
                OdemeId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
                Durum = DurumEnum.KayıtEdildi
            };
            entity.OdemeTarih = model.OdemeTarih;
            entity.OdemeTarihString = model.OdemeTarihString;
            entity.OdemeTuru = model.OdemeTuru;
            entity.FirmaAdi = model.FirmaAdi;
            entity.Kime = model.Kime;
            entity.OdenecekTutar = model.OdenecekTutar;

            await _context.Odeme.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.Odeme.Where(x => !x.Silindi && x.OdemeId == id).FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = entity.OdemeId, Message = ApiResultMessages.ODE001 };

            entity.Silindi = true;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.OdemeId, Message = ApiResultMessages.Ok };
        }

        public async Task<ICollection<OdemeGetDto>> Get()
        {
            var entity = new List<OdemeGetDto>();
            entity = await _context.Odeme
                .Where(x => !x.Silindi && x.Durum==DurumEnum.KayıtEdildi)
                .Select(s => new OdemeGetDto
                {
                    OdemeId=s.OdemeId,
                    FirmaAdi=s.FirmaAdi,
                    OdemeTarih=s.OdemeTarih,
                    Olusturuldu=s.Olusturuldu,
                    OdenecekTutar=s.OdenecekTutar,
                    Kime=s.Kime,
                    Durum=s.Durum,
                    OdemeTuru=s.OdemeTuru,
                    OdemeTuruString=s.OdemeTuruString,
                    OdemeTarihString=s.OdemeTarihString
                })
                .ToListAsync();

            return entity;
        }

        public async Task<ApiResult> Odendi(Guid id)
        {
            var entity = await _context.Odeme
                .Where(x => !x.Silindi && x.Durum==DurumEnum.KayıtEdildi && x.OdemeId == id ).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.OdemeId, Message = ApiResultMessages.ODE001 };

            entity.Durum = DurumEnum.Odendi;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.OdemeId, Message = ApiResultMessages.Ok };

        }

        public async Task<ApiResult> Update(OdemeUpdateDto model)
        {
            var entity = await _context.Odeme
                .Where(x => !x.Silindi && x.OdemeId == model.OdemeId).FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = model.OdemeId, Message = ApiResultMessages.ODE001 };

            entity.FirmaAdi = model.FirmaAdi;
            entity.Kime = model.Kime;
            entity.OdemeTarih = model.OdemeTarih;
            entity.OdemeTuru = model.OdemeTuru;
            entity.OdenecekTutar = model.OdenecekTutar;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity, Message = ApiResultMessages.Ok };
        }


    }
}
