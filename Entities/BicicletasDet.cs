using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class BicicletasDet
{
    public Guid VehicleId { get; set; }

    public string? TipoBicicleta { get; set; }

    public string? MaterialMarco { get; set; }

    public string? Frenos { get; set; }

    public bool? Cambios { get; set; }

    public byte? NumeroCambios { get; set; }

    public string? Suspension { get; set; }

    public string? Aro { get; set; }

    public virtual Vehiculo Vehicle { get; set; } = null!;
}
