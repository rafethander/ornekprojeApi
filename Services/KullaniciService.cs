using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirinWebApi.Services
{
    public interface IKullaniciService
    {
        Task<ApiResult> Authenticate(KullaniciAuthenticateDto model);
        Task<ApiResult> Add(KullaniciAddDto model);
        Task<ApiResult> Update(KullaniciUpdateDto model);
        Task<ApiResult> Delete(Guid id);
        Task<IEnumerable<KullaniciGetDto>> Get();

    }
    public class KullaniciService : IKullaniciService
    {
        private readonly FirinWebApiDbContext _context;
        private readonly AppSettings _appSettings;
        public KullaniciService(FirinWebApiDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public async Task<ApiResult> Authenticate(KullaniciAuthenticateDto model)
        {
            var entity = await _context.Kullanici.Where(x => !x.Silindi && x.KullaniciAdi == model.KullaniciAdi).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.KullaniciAdi, Message = ApiResultMessages.KUE001 };

            if(VerifyPasswordHash(model.Sifre, entity.SifreHash, entity.SifreSalt)==false)
                return new ApiResult { Data = entity.KullaniciAdi, Message = ApiResultMessages.KSE001 };
            
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, entity.KullaniciAdi),
                    new Claim(ClaimTypes.Role,entity.Role)
                    
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            entity.Token = tokenHandler.WriteToken(token);

            return new ApiResult { Data = new { model.KullaniciAdi, entity.Token }, Message = ApiResultMessages.Ok };

        }
        public async Task<ApiResult> Add(KullaniciAddDto model)
        {
            //Validation
            if (await _context.Kullanici.AnyAsync(x => x.KullaniciAdi == model.KullaniciAdi))
                return new ApiResult { Data = model.KullaniciAdi, Message = ApiResultMessages.KAW001 };
            

            byte[] sifreHash, sifreSalt;

            CreatePasswordHash(model.Sifre,out sifreHash,out sifreSalt);

            var entity = new Kullanici
            {
                KullaniciId = Guid.NewGuid(),
                Olusturuldu = DateTime.UtcNow,
            };

            entity.Ad = model.Ad;
            entity.Soyad = model.Soyad;
            entity.KullaniciAdi = model.KullaniciAdi;
            entity.SifreHash =sifreHash;
            entity.SifreSalt = sifreSalt;
            entity.Role = model.Role;

            await _context.Kullanici.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.KullaniciAdi, Message = ApiResultMessages.Ok };


        }

        public async Task<ApiResult> Update(KullaniciUpdateDto model)
        {
            var entity = await _context.Kullanici.Where(x => !x.Silindi && x.KullaniciId == model.KullaniciId).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.KullaniciId, Message = ApiResultMessages.KUE001 };

            byte[] sifreHash, sifreSalt;

            CreatePasswordHash(model.Sifre, out sifreHash, out sifreSalt);

            entity.Ad = model.Ad;
            entity.Soyad = model.Soyad;

            if (await _context.Kullanici.AnyAsync(x => !x.Silindi && x.KullaniciAdi == model.KullaniciAdi))
                return new ApiResult { Data = model.KullaniciAdi, Message = ApiResultMessages.KAW001 };

            entity.KullaniciAdi = model.KullaniciAdi;
            entity.SifreHash =sifreHash;
            entity.SifreSalt = sifreSalt;
            entity.Role = model.Role;
            
            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.KullaniciId, Message = ApiResultMessages.Ok };

        }

        public async Task<ApiResult> Delete(Guid id)
        {
            var entity = await _context.Kullanici.Where(x => !x.Silindi && x.KullaniciId == id).FirstOrDefaultAsync();
            if (entity == null)
                return new ApiResult { Data = entity.KullaniciId, Message = ApiResultMessages.KUE001 };

            entity.Silindi = true;

            await _context.SaveChangesAsync();

            return new ApiResult { Data = entity.KullaniciId, Message = ApiResultMessages.Ok };

        }

        public async Task<IEnumerable<KullaniciGetDto>> Get()
        {
            var entity = await (from k in _context.Kullanici
                                where k.Silindi == false
                                select new KullaniciGetDto
                                {
                                    Ad = k.Ad,
                                    Soyad = k.Soyad,
                                    KullaniciAdi = k.KullaniciAdi,
                                    Role=k.Role
                                }).ToListAsync();
            return entity;
        }



        private static void CreatePasswordHash(string sifre, out byte[] sifreHash,out byte[] sifreSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            sifreSalt = hmac.Key;
            sifreHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sifre));
        }

        private static bool VerifyPasswordHash(string sifre,byte[] gercekSifreHash,byte[] gercekSifreSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(gercekSifreSalt);
            var dogrulananSifreHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sifre));

            for (int i = 0; i < dogrulananSifreHash.Length; i++)
            {
                if (dogrulananSifreHash[i] != gercekSifreHash[i]) return false;
            }

            return true;
        } 

        
    }
}
