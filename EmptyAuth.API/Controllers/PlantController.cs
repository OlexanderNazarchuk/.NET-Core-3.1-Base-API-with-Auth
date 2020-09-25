using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyAuth.API.Attributes;
using EmptyAuth.API.Extensions;
using EmptyAuth.Common.Enums.Claims;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Models;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmptyAuth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PlantController : ControllerBase
    {

        private readonly IPlantService _plantService;
        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }
        [HttpPut, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Create(PlantDto requestModel)
        {
            var request = HttpContext.GetRequestData(requestModel);
            await _plantService.CreateAsync(request);
            return Ok("Success");
        }
        [HttpDelete, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]

        public async Task<IActionResult> Delete(PlantDto requestModel)
        {
            var request = HttpContext.GetRequestData(requestModel);
            await _plantService.DeleteAsync(request);
            return Ok("Success");
        }
        [HttpPost, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]

        public async Task<IActionResult> Update(PlantDto requestModel)
        {
            var request = HttpContext.GetRequestData(requestModel);
            await _plantService.UpdateAsync(request);
            return Ok("Success");
        }
        [HttpGet, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Get()
        {
            var request = HttpContext.GetRequestData();
            var plants = await _plantService.GetAllAsync(request);
            return Ok(plants);
        }
    }

}