using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public required User User { get; set; }
        public string? Token { get; set; }
        public bool IsAuth { get; set; }
    }
}
