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
    [Route("api/[Controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;
        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }


        //POST: api/Musteri/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]MusteriAddDto model)
        {
            var result = await _musteriService.Add(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);
            
                return Ok(result);            
        }

        //GET: api/Musteri/Get
        [HttpGet("Get")]
        //[ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get()
        {

            var result = await _musteriService.Get();

            return Ok(result);
        }

        //GET: api/Musteri/SelectGet
        [HttpGet("SelectGet")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> SelectGet()
        {
            var result = await _musteriService.SelectGet();

            return Ok(result);
        }

        //PUT: api/Musteri/Update
        [HttpPut("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update([FromBody]MusteriUpdateDto model)
        {
            var result = await _musteriService.Update(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);
            return Ok(result);

        }

        //DELETE: api/Musteri/Delete/id
       [HttpDelete("Delete/{id}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _musteriService.Delete(id);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);
            return Ok(result);
        }
    }
}