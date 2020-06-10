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
using Microsoft.AspNetCore.Routing;

namespace FirinWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    
    public class AlinanUrunController : ControllerBase
    {
        private readonly IAlinanUrunService _alinanUrun;
        public AlinanUrunController(IAlinanUrunService alinanUrun)
        {
            _alinanUrun = alinanUrun;
        }


        //POST: api/AlinanUrun/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]AlinanUrunAddDto model)
        {
            var result = await _alinanUrun.Add(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);
            return Ok(result);
        }

        //GET: api/AlinanUrun/Get
        [HttpGet("Get")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get()
        {
            var result = await _alinanUrun.Get();

            return Ok(result);
        }

        ////GET: api/AlinanUrun/JrGet
        //[HttpGet("JrGet")]
        //public async Task<IActionResult> JrGet()
        //{
        //    var result = await _alinanUrun.JrGet();

        //    return Ok(result);
        //}

        //PUT: api/AlinanUrun/Update
        [HttpPut("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update([FromBody]AlinanUrunUpdateDto model)
        {
            var result = await _alinanUrun.Update(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //DELETE: api/AlinanUrun/Delete/id
        [HttpDelete("Delete/{id}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _alinanUrun.Delete(id);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }
    }
}