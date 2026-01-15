using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;
using API_VehiclesAPP.Services.Interfaces;
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
            if (string.IsNullOrEmpty(registerRequest.Email)) return BadRequest();

            var resp = await _authService.Register(registerRequest);

            return Created("", resp);
        }

        [HttpPost("register/complete")]
        public async Task<IActionResult> CompleteRegister([FromBody]RegisterResponseDTO registerResponse)
        {
            if(registerResponse.UserId == Guid.Empty || registerResponse.Cliente == null)
                return BadRequest();


            registerResponse.Cliente!.UserId = registerResponse.UserId;
            var resp = await _authService.CompleteDataUser( registerResponse.Cliente );

            return Ok(resp);
        }
        


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO.Email == null || loginRequestDTO.Password == null) 
            {
                return BadRequest();
            }

            _ = await _authService.Login(loginRequestDTO);

            return Ok();
        }





    }
}
