using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

    }
}
