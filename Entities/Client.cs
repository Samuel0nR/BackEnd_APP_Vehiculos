using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class Client
{
    public Guid ClienteId { get; set; }

    public Guid UserId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Rut { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual User User { get; set; } = null!;
}
