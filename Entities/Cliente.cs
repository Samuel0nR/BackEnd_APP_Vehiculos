using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_VehiclesAPP.Entities;

public partial class Cliente
{
    public Guid? ClienteId { get; set; }

    public Guid UserId { get; set; } = Guid.Empty;
    
    [Required]
    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    [Required]
    public string? Rut { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion {  get; set; }

}
