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
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class SatilanUrunController : ControllerBase
    {

        private readonly ISatilanUrunService _satilanUrun;
        public SatilanUrunController(ISatilanUrunService satilanUrun)
        {
            _satilanUrun = satilanUrun;
        }

        //POST: api/SatilanUrun/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]SatilanUrunAddDto model)
        {
            var result = await _satilanUrun.Add(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //GET: api/SatilanUrun/Get
        [HttpGet("Get")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get()
        {
            var result =await _satilanUrun.Get();

            return Ok(result);
        }

        //PUT: api/SatilanUrun/Update
        [HttpPut("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update([FromBody]SatilanUrunUpdateDto model)
        {
            var result = await _satilanUrun.Update(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //DELETE: api/SatilanUrun/Delete/id
        [HttpDelete("Delete/{id}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _satilanUrun.Delete(id);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }
    }
}