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
    
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _faturaService;
        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }



        //POST: api/Fatura/Add
        [HttpPost("Add")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Add([FromBody]FaturaAddDto model)
        {
            var result = await _faturaService.Add(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
            
        }

        //POST: api/Fatura/DirektAdd
        [HttpPost("DirektAdd")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> DirektAdd([FromBody]IrsaliyeAddDto model)
        {
            var result = await _faturaService.DirektAdd(model);
            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);

        }


        //POST: api/Fatura/Get
        [HttpPost("Get")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Get([FromBody]FaturaGetDtoModel model)
        {
            var result = await _faturaService.Get(model);

            return Ok(result);
        }

        //PUT: api/Fatura/Update
        [HttpPut("Update")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Update(FaturaUpdateDto model)
        {
            var result = await _faturaService.Update(model);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }

        //Delete: api/Fatura/Delete/5
        [HttpDelete("Delete/{faturaNo}")]
        [ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> Delete(int faturaNo)
        {
            var result = await _faturaService.Delete(faturaNo);

            if (result.Message != ApiResultMessages.Ok)
                return BadRequest(result);

            return Ok(result);
        }


        //POST: api/Fatura/GetWithFaturaNo
        [HttpPost("GetWithFaturaNo/{faturaNo}")]
        //[ServiceFilter(typeof(AdminFilter))]
        public async Task<IActionResult> GetWithFaturaNo(int faturaNo)
        {
            var result = await _faturaService.GetWithFaturaNo(faturaNo);

            return Ok(result);
        }
    }
}