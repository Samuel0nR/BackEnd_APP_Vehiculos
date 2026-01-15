using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequest);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);

        Task<LoginResponseDTO> CompleteDataUser(Cliente user);
    }
}
