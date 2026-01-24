using System.Security.Claims;
using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;
using API_VehiclesAPP.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_VehiclesAPP.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequest)
        {
            if (string.IsNullOrEmpty(registerRequest.Email)) 
                return BadRequest();

            var resp = await _authService.Register(registerRequest);

            return Created("", resp);
        }

        [Authorize]
        [HttpPost("register/complete")]
        public async Task<IActionResult> CompleteRegister([FromBody]ClientDTO client)
        {
            if(string.IsNullOrEmpty(client.Nombre) || string.IsNullOrEmpty(client.Rut))
                return BadRequest();

            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            var resp = await _authService.CompleteDataUser( userId, client );

            return Ok(resp);
        }
        


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO.Email == null || loginRequestDTO.Password == null) 
                return BadRequest();            

            var result = await _authService.Login(loginRequestDTO);

            return Ok(result);
        }





    }
}
