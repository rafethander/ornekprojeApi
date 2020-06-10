using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirinWebApi.Database;
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
    
    public class IrsaliyeController : ControllerBase
    {
        private readonly IIrsaliyeService _irsaliyeService;
        
        public IrsaliyeController(IIrsaliyeService irsaliyeService,FirinWebApiDbContext context)
        {
            _irsaliyeService = irsaliyeService;
            
        }

        //POST: api/Irsaliye/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]IrsaliyeAddDto model)
        {
            var result = await _irsaliyeService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //POST: api/Irsaliye/Update
        [HttpPost("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update([FromBody]IrsaliyeUpdateDto model)
        {
            var result = await _irsaliyeService.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }



        //POST: api/Irsaliye/Get
        [HttpPost("Get")]
        [ServiceFilter(typeof(AdminFilter))]
        public async  Task<IActionResult> Get([FromBody]IrsaliyeGetModelDto model)
        {
            var result = await _irsaliyeService.Get(model);

            return Ok(result);
        }

        //POST: api/Irsaliye/GetWithMusteriAdiAndIrsaliyeNo
        [HttpPost("GetWithMusteriAdiAndIrsaliyeNo")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> GetWithMusteriAdiAndIrsaliyeNo([FromBody]IrsaliyeGetModelDto model)
        {
            var result = await _irsaliyeService.GetWithMusteriAdiAndIrsaliyeNo(model);

            return Ok(result);
        }
        //Delete: api/Irsaliye/Delete /irsaliyeNo
        [HttpDelete("Delete/{irsaliyeNo}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(int irsaliyeNo)
        {
            var result = await _irsaliyeService.Delete(irsaliyeNo);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //Post: api/Irsaliye/GetWithIrsaliyeNo/1
        [HttpPost("GetWithIrsaliyeNo/{irsaliyeNo}")]
        public async Task<IActionResult> GetWithIrsaliyeNo(int irsaliyeNo)
        {
            var result = await _irsaliyeService.GetWithIrsaliyeNo(irsaliyeNo);

            return Ok(result);
        }


    }
}