using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmptyAuth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _authService.LoginAsync(username, password);
            if (string.IsNullOrEmpty(result))
                return BadRequest();
            return Ok(result);
        }


        [HttpPost, MapToApiVersion("1")]
        [ProducesResponseType(typeof(string), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Create(RegisterModel request)
        {
            await _authService.CreateAsync(request.Username, request.Password, request.OrganizationId);
            return Ok();
        }
    }

}