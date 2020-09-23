using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyAuth.API.Attributes;
using EmptyAuth.Common.Enums.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmptyAuth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        [HttpPut, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        [ClaimAuthorizeAttribute(Organization.Create)]
        public async Task<IActionResult> Create()
        {
            return Ok("Success");
        }
        [HttpDelete, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        [ClaimAuthorizeAttribute(Organization.Delete)]

        public async Task<IActionResult> Delete()
        {
            return Ok("Success");
        }
        [HttpPost, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        [ClaimAuthorizeAttribute(Organization.Update)]

        public async Task<IActionResult> Update(OrganizationRequest request)
        {
            return Ok("Success");
        }
        [HttpGet, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        [ClaimAuthorizeAttribute(Organization.Read)]
        public async Task<IActionResult> Get()
        {
            return Ok("Success");
        }
    }

    public class OrganizationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}