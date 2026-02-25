using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.DTOs
{
    public class ClientDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        public string Rut { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        [Required]
        public required string Email {  get; set; } = string.Empty;
        public string Username {  get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
        
        [Required]
        public required Guid UserId {  get; set; }

    }
}
