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
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class TahsilatController : ControllerBase
    {
        private readonly ITahsilatService _tahsilatService;
        public TahsilatController(ITahsilatService tahsilatService)
        {
            _tahsilatService = tahsilatService;
        }


        //POST: api/Tahsilat/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]TahsilatAddDto model)
        {
            var result = await _tahsilatService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);

        }

        //GET: api/Tahsilat/Get/3
        [HttpGet("Get/{musteriId}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get(Guid musteriId)
        {
            var result = await _tahsilatService.Get(musteriId);

            return Ok(result);
        }

        //DELETE: api/Tahsilat/Delete/5
        [HttpDelete("Delete/{tahsilatId}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(Guid tahsilatId)
        {
            var result = await _tahsilatService.Delete(tahsilatId);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //GET: api/Tahsilat/FaturaToplamTutar/3
        [HttpGet("FaturaToplamTutar/{musteriId}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> FaturaToplamTutar(Guid musteriId)
        {
            var result = await _tahsilatService.ToplamFaturaTutar(musteriId);

            return Ok(result);
        }
    }
}