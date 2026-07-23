using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUserRepository repository) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register(CreateUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await repository.Register(dto);
            return result.Flage ? Ok(result.Flage) : BadRequest(Request);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response>> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await repository.Login(dto);
            return result.Flage ? Ok(result) : BadRequest(Request);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Response>> Get([FromRoute ] Guid Id)
        {
            var result = await repository.GetUser(Id);
            return result is not null  ? Ok(result) : BadRequest(Request);
        }
    }
}
