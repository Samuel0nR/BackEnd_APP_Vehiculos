using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace API_VehiclesAPP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/email")]
    public class EmailsController(IEmailService emailService) : ControllerBase
    {
        private readonly IEmailService _emailService = emailService;

        [AllowAnonymous]
        [HttpPost("resend-mymobi")]
        public async Task<IActionResult> Register(ResendRequestDTO requestDTO)
        {
            var resp = await _emailService.SendEmailResend(requestDTO);

            return Ok(resp);
        }

    }
}
