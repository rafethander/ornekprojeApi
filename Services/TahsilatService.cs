using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Services
{
    public interface ITahsilatService
    {
        public Task<ApiResult> Add(TahsilatAddDto model);
        public Task<ApiResult> Delete(Guid tahsilatId);
        public Task<ICollection<TahsilatGetDto>> Get(Guid musteriId);

        public Task<ApiResult> ToplamFaturaTutar(Guid musteriId);
    }
    public class TahsilatService:ITahsilatService
    {
        private readonly FirinWebApiDbContext _context;
        public TahsilatService(FirinWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> Add(TahsilatAddDto model)
        {
            var entityMusteri = await _context.Musteri.Where(x => x.MusteriId == model.MusteriId).FirstOrDefaultAsync();
            if (entityMusteri == null)
                return new ApiResult { Data = model.Musteri.MusteriAdi, Message = ApiResultMessages.MUE001 };

            var entityTahsilat = new Tahsilat
            {
                TahsilatId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow
            };

            entityTahsilat.TahsilatTarihi = model.TahsilatTarihi;
            entityTahsilat.TahsilatTuru = model.TahsilatTuru;
            entityTahsilat.TahsilatTutar = model.TahsilatTutar;
            entityTahsilat.MusteriId = model.MusteriId;
            entityTahsilat.Musteri = entityMusteri;

            await _context.Tahsilat.AddAsync(entityTahsilat);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entityTahsilat.TahsilatId, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid tahsilatId)
        {
            var entity = await _context.Tahsilat.Where(x => x.TahsilatId == tahsilatId).FirstOrDefaultAsync();

            if (entity == null)
                return new ApiResult { Data = tahsilatId, Message = ApiResultMessages.TAW001 };

            entity.Silindi = true;
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.TahsilatId, Message = ApiResultMessages.Ok };

        }

        public async Task<ICollection<TahsilatGetDto>> Get(Guid musteriId)
        {
            var entity = await (from t in _context.Tahsilat
                                where t.Silindi==false && t.MusteriId==musteriId
                                orderby t.Olusturuldu descending
                                select new TahsilatGetDto
                                {
                                    TahsilatId = t.TahsilatId,
                                    Olusturuldu = t.Olusturuldu,
                                    TahsilatTarihi = t.TahsilatTarihi,
                                    TahsilatTarihiString = t.TahsilatTarihiString,
                                    TahsilatTuru = t.TahsilatTuru,
                                    TahsilatTuruString=t.TahsilatTuruString,
                                    TahsilatTutar = t.TahsilatTutar,
                                    MusteriId = t.MusteriId,
                                    MusteriAdi = t.Musteri.MusteriAdi
                                }).ToListAsync();

            return entity;
        }

        public async Task<ApiResult> ToplamFaturaTutar(Guid musteriId)
        {
            var entityFatura =await (from f in _context.Fatura
                          join i in _context.Irsaliye on f.FaturaId equals i.Fatura.FaturaId
                          join Mi in _context.MusteriIrsaliye on i.IrsaliyeId equals Mi.IrsaliyeId
                          where (f.Silindi == false && Mi.MusteriId == musteriId)
                          select new FaturaToplamTutar
                          {
                              FaturaNo=f.FaturaNo,
                              ToplamTutar =f.ToplamTutar
                          }).ToListAsync() ;

            var TutarListe = entityFatura.GroupBy(f => f.FaturaNo).Select(f => f.First());
            var ToplamTutarListe = TutarListe.Sum(f => f.ToplamTutar);


            return new ApiResult { Data = ToplamTutarListe, Message = ApiResultMessages.Ok };
        }
    }
}
