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
    public interface ISatilanUrunService
    {
        Task<ApiResult> Add(SatilanUrunAddDto model);
        Task<ICollection<SatilanUrunGetDto>> Get();
        Task<ApiResult> Update(SatilanUrunUpdateDto model);
        Task<ApiResult> Delete(Guid id);

    }
    public class SatilanUrunService : ISatilanUrunService
    {
        private readonly FirinWebApiDbContext _context;
        public SatilanUrunService(FirinWebApiDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult> Add(SatilanUrunAddDto model)
        {
            var entity = new SatilanUrun
            {
                SatilanUrunId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
            };

            entity.UrunAdi = model.UrunAdi;
            entity.Birim = model.Birim;
            entity.Fiyat = model.Fiyat;
            entity.Kdv = model.Kdv;

            await _context.SatilanUrun.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity, Message = ApiResultMessages.Ok };

        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.SatilanUrun.Where(x => !x.Silindi && x.SatilanUrunId == id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.SatilanUrunId, Message = ApiResultMessages.SUE001 };

            entity.Silindi = true;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.SatilanUrunId, Message = ApiResultMessages.Ok };
        }

        public async Task<ICollection<SatilanUrunGetDto>> Get()
        {
            var entity = new List<SatilanUrunGetDto>();
            entity = await _context.SatilanUrun
                .Where(x => !x.Silindi)
                .Select(s => new SatilanUrunGetDto
                {
                    SatilanUrunId = s.SatilanUrunId,
                    Birim = s.Birim,
                    Fiyat = s.Fiyat,
                    Kdv = s.Kdv,
                    UrunAdi = s.UrunAdi
                })
                .ToListAsync();

            return entity;
        }

        public async Task<ApiResult> Update(SatilanUrunUpdateDto model)
        {
            var entity = await _context.SatilanUrun
                    .Where(x => !x.Silindi && x.SatilanUrunId == model.SatilanUrunId)
                    .FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.SatilanUrunId, Message = ApiResultMessages.SUE001 };

            entity.UrunAdi = model.UrunAdi;
            entity.Birim = model.Birim;
            entity.Fiyat = model.Fiyat;
            entity.Kdv = model.Kdv;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.SatilanUrunId, Message = ApiResultMessages.Ok };
        }
    }
}
