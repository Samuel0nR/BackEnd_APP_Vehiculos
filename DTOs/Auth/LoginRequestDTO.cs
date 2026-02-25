using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.DTOs.Auth
{
    public class LoginRequestDTO
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        public string Password { get; set; } = null!;
        
        [Required(ErrorMessage = "El Email es Obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = null!;

    }
}
