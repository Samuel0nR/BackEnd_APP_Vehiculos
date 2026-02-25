using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.DTOs
{
    public class ResendRequestDTO
    {
        public string From { get; set; } = "Mymobi <no-reply@mymobi.cl>";

        [Required(ErrorMessage = "El destinatario es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string To { get; set; } = "admi.mymoby@gmail.com";

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string HtmlBody { get; set; } = string.Empty;
    }
}
