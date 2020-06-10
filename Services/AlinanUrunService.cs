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
    public interface IAlinanUrunService
    {
        Task<ApiResult> Add(AlinanUrunAddDto model);
        Task<ICollection<AlinanUrunGetDto>> Get();
        //Task<ICollection<AlinanUrunJrGetDto>> JrGet();
        Task<ApiResult> Update(AlinanUrunUpdateDto Model);
        Task<ApiResult> Delete(Guid id);
    }
    public class AlinanUrunService : IAlinanUrunService
    {

        private readonly FirinWebApiDbContext _context;
        public AlinanUrunService(FirinWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> Add(AlinanUrunAddDto model)
        {
            var entity = new AlinanUrun
            {
                AlinanUrunId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
            };

            entity.UrunAdi = model.UrunAdi;
            entity.TedarikciAdi = model.TedarikciAdi;
            entity.Birim = model.Birim;
            entity.KDV = model.KDV;
            entity.Miktar = model.Miktar;
            entity.AlimTarih = model.AlimTarih;
            entity.Fiyat = model.Fiyat;
            entity.AlimTarihString = model.AlimTarihString;

            await _context.AlinanUrun.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity, Message = ApiResultMessages.Ok };
        }


        public async Task<ICollection<AlinanUrunGetDto>> Get()
        {
            var result = new List<AlinanUrunGetDto>();

            result = await _context.AlinanUrun
                .Where(x => !x.Silindi)
                .Select(s => new AlinanUrunGetDto
                {
                    AlinanUrunId = s.AlinanUrunId,
                    UrunAdi = s.UrunAdi,
                    TedarikciAdi = s.TedarikciAdi,
                    AlimTarih = s.AlimTarih,
                    AlimTarihString=s.AlimTarihString,
                    Birim = s.Birim,
                    Fiyat = s.Fiyat,
                    Miktar = s.Miktar,
                    KDV = s.KDV
                })
                .ToListAsync();

            return result;
        }

        //public async Task<ICollection<AlinanUrunJrGetDto>> JrGet()
        //{
        //    var result = new List<AlinanUrunJrGetDto>();
        //    result = await _context.AlinanUrun
        //        .Where(x => !x.Silindi)
        //        .Select(s => new AlinanUrunJrGetDto
        //        {
        //            TedarikciAdi=s.TedarikciAdi,
        //            AlimTarih=s.AlimTarih,
        //            Fiyat=s.Fiyat,
        //            UrunAdi=s.UrunAdi,
        //        })
        //        .ToListAsync();

        //    return result;
        //}

        public async Task<ApiResult> Update(AlinanUrunUpdateDto Model)
            {
            var entity = await _context.AlinanUrun.Where(x => !x.Silindi && x.AlinanUrunId == Model.AlinanUrunId).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.AlinanUrunId, Message = ApiResultMessages.AUE001 };
            // _context.Entry<AlinanUrun>(entity).State = EntityState.Modified;

            entity.TedarikciAdi = Model.TedarikciAdi;
            entity.UrunAdi = Model.UrunAdi;
            entity.Fiyat = Model.Fiyat;
            entity.AlimTarih = Model.AlimTarih;
            entity.Birim = Model.Birim;
            entity.KDV = Model.KDV;
            entity.Miktar = Model.Miktar;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.AlinanUrunId, Message = ApiResultMessages.Ok };

        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.AlinanUrun.Where(x => !x.Silindi && x.AlinanUrunId == id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.AlinanUrunId, Message = ApiResultMessages.AUE001 };

            entity.Silindi = true;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.AlinanUrunId, Message = ApiResultMessages.Ok };
        }
    }
}
