using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.DTOs
{
    public class ClientDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        [Required]
        public string Rut { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }
}
