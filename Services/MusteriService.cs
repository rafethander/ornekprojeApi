using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using FirinWebApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirinWebApi.Services
{
    public interface IMusteriService
    {
        Task<ApiResult> Add(MusteriAddDto model);
        Task<ApiResult> Update(MusteriUpdateDto model);
        Task<ApiResult> Delete(Guid id);
        Task<ICollection<MusteriGetDto>> Get();
        Task<ICollection<MusteriSelectDto>> SelectGet();
        
    }


    public class MusteriService : IMusteriService
    {
        private readonly FirinWebApiDbContext _context;
        public MusteriService(FirinWebApiDbContext context)
        {
            _context = context;
        }


        public async Task<ApiResult> Add(MusteriAddDto model)
        {
            //Validation
            if (await _context.Musteri.AnyAsync(x =>!x.Silindi && x.MusteriAdi == model.MusteriAdi))
                return new ApiResult { Data = model.MusteriAdi, Message = ApiResultMessages.MUW001 };
            var musteri = new Musteri
            {
                MusteriId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow
            };

            musteri.MusteriAdi = model.MusteriAdi;
            musteri.Adres = model.Adres;
            musteri.Aciklama = model.Aciklama;
            musteri.VergiDaire = model.VergiDaire;
            musteri.VergiDaireNo = model.VergiDaireNo;

            await _context.Musteri.AddAsync(musteri);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = musteri, Message = ApiResultMessages.Ok };
        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity =await _context.Musteri.Where(x => !x.Silindi && x.MusteriId == id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.MusteriId, Message = ApiResultMessages.MUE001 };

            entity.Silindi = true;
            await _context.SaveChangesAsync();
            return new ApiResult { Data = "", Message = ApiResultMessages.Ok };
        }

        public async Task<ICollection<MusteriGetDto>> Get()
        {
            var result = new List<MusteriGetDto>();
            result = await _context.Musteri
                        .Where(x => !x.Silindi)
                        .Select(s => new MusteriGetDto
                        {
                            MusteriId=s.MusteriId,
                            MusteriAdi=s.MusteriAdi,
                            Adres=s.Adres,
                            VergiDaire=s.VergiDaire,
                            VergiDaireNo=s.VergiDaireNo,
                            Aciklama=s.Aciklama
                        }).ToListAsync();
            return result;
        }

        public async Task<ICollection<MusteriSelectDto>> SelectGet()
        {

            var result = new List<MusteriSelectDto>();
            result = await _context.Musteri.Where(x => !x.Silindi)
                .Select(s => new MusteriSelectDto
                {
                    MusteriId = s.MusteriId,
                    MusteriAdi = s.MusteriAdi
                }).ToListAsync();

            return result;
        }

        public async Task<ApiResult> Update(MusteriUpdateDto model)
        {
            var entity=await _context.Musteri
                            .Where(x => !x.Silindi && x.MusteriId == model.MusteriId)
                            .FirstOrDefaultAsync();
            if (entity== null)
                return new ApiResult { Data = entity.MusteriId, Message = ApiResultMessages.MUE001 };

            

            entity.MusteriAdi = model.MusteriAdi;
            entity.Adres = model.Adres;
            entity.Aciklama = model.Aciklama;
            entity.VergiDaire = model.VergiDaire;
            entity.VergiDaireNo = model.VergiDaireNo;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity, Message = ApiResultMessages.Ok };


        }
    }
}
