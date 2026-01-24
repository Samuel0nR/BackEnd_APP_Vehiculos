using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.DTOs.Auth
{
    public class RegisterResponseDTO
    {
        public string Email { get; set; } = null!;
        public Guid UserId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
