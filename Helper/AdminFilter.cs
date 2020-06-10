using FirinWebApi.Database;
using FirinWebApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FirinWebApi.Helper
{
    public class AdminFilter : IActionFilter
    {
        private readonly FirinWebApiDbContext _context;
        public AdminFilter(FirinWebApiDbContext context)
        {
            _context = context;
        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            var token = request.Headers["Authorization"];

            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode=(int)HttpStatusCode.Unauthorized,
                };

                return;
            }

            var tokenClaims = jsonToken.Claims.ToList();
            

            var kullaniciAdi = _context.Kullanici.Where(x => !x.Silindi && x.KullaniciAdi == tokenClaims[0].Value).FirstOrDefault();

            if (kullaniciAdi == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };

                return;
            }

            var kullaniciRole = _context.Kullanici.Where(x => !x.Silindi && x.Role == tokenClaims[1].Value).FirstOrDefault();

            if (kullaniciRole == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };

                return;
            }

            var expireDate = jsonToken.ValidTo.ToLocalTime();

            if (expireDate < DateTime.Now)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };

                return;
            }
            
           




        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
