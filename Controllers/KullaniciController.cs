using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirinWebApi.Database.Models;
using FirinWebApi.DTOs;
using FirinWebApi.Helper;
using FirinWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace FirinWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullanici;
        public KullaniciController(IKullaniciService kullanici)
        {
            _kullanici = kullanici;
        }


        //POST: api/Kullanici/Add
        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]KullaniciAddDto model)
        {
            var result = await _kullanici.Add(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);
            return Ok(result);
        }


        //PUT: api/Kullanici/Update
        [Authorize(Roles =Role.Admin)]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]KullaniciUpdateDto model)
        {
            var result = await _kullanici.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //DELETE: api/Kullanici/Delete/id
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _kullanici.Delete(id);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }


        //POST: api/Kullanici/Authenticate
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]KullaniciAuthenticateDto model)
        {
            var result = await _kullanici.Authenticate(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //GET: api/Kullanici/Get
        [Authorize(Roles = Role.Admin)]
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _kullanici.Get();

            return Ok(result);
        }



        //Araştır. Admin hepsine ulaşır , kullanici sadce kendikine ulasabilir mi?
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    // only allow admins to access other user records
        //    var currentUserId = int.Parse(User.Identity.Name);
        //    if (id != currentUserId && !User.IsInRole(Role.Admin))
        //        return Forbid();

        //    var user = _userService.GetById(id);

        //    if (user == null)
        //        return NotFound();

        //    return Ok(user);
        //}
    }
}
