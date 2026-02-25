using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Register(RegisterRequestDTO registerRequest);
        Task<ResponseDTO> CompleteDataUser(Guid userId, ClientDTO client);

        Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequest);

        Task<AuthStatusDTO> CheckStatus(Guid userId);


    }
}
