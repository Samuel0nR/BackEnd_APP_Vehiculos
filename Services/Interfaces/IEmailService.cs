using API_VehiclesAPP.DTOs;
using Resend;

namespace API_VehiclesAPP.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ResendResponse> SendEmailResend(ResendRequestDTO resendRequest);
    
    }
}
