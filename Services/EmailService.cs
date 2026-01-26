using System.IO;
using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.Services.Interfaces;
using Resend;

namespace API_VehiclesAPP.Services
{
    public class EmailService(IResend resend, IWebHostEnvironment webHost, ILogger<EmailService> logger) : IEmailService
    {
        private readonly IResend _resend = resend;
        private readonly IWebHostEnvironment _webHost = webHost;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task<ResendResponse> SendEmailResend(ResendRequestDTO resendRequest)
        {
            try
            {   
                var body = Path.Combine(_webHost.ContentRootPath, "Resources", "TemplatesHTML", "EmailTemplate.html");
                var htmlContent = File.ReadAllText(body);

                /*
                    1.- Buscar Nombre del Cliente
                    2.- Modificar Variables del Template
                */

                htmlContent = htmlContent.Replace("{{Year}}", DateTime.Now.Year.ToString());

                var email = new EmailMessage()
                {
                    From = "onboarding@resend.dev",
                    To =  resendRequest.To,
                    Subject = resendRequest.Subject,
                    HtmlBody = htmlContent,
                };

                var resp = await _resend.EmailSendAsync(email);

                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Enviar Correo : {ex.Message}. ", resendRequest);
                throw;
            }
        }



    }
}
