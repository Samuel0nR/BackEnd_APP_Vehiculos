using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.DTOs.Auth
{
    public class LoginResponseDTO : ResponseDTO
    {
        public required ClientDTO User { get; set; }
        public string? Token { get; set; }
        public bool IsAuth { get; set; }
        public bool ProfileComplete { get; set; }
    }
}
