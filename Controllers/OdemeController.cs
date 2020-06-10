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
    
    public class OdemeController : ControllerBase
    {
        private readonly IOdemeService _odemeService;
        public OdemeController(IOdemeService odemeService)
        {
            _odemeService = odemeService;
        }

        //POST: api/Odeme/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]OdemeAddDto model)
        {
            var result = await _odemeService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //GET: api/Odeme/Get
        [HttpGet("Get")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get()
        {
            var result = await _odemeService.Get();

            return Ok(result);
        }

        //PUT: api/Odeme/Update
        [HttpPut("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update([FromBody]OdemeUpdateDto model)
        {
            var result = await _odemeService.Update(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //DELETE: api/Odeme/Delete/{id}
        [HttpDelete("Delete/{id}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _odemeService.Delete(id);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //PUT: api/Odeme/Odendi
        [HttpPut("Odendi/{id}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Odendi(Guid id)
        {
            var result = await _odemeService.Odendi(id);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }
    }
}