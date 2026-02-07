using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class AutosDet
{
    public Guid VehicleId { get; set; }

    public string Combustible { get; set; } = null!;

    public string? Motor { get; set; }

    public long? Kilometraje { get; set; }

    public string? Transmision { get; set; }

    public byte? Puertas { get; set; }

    public string? Llanta { get; set; }

    public string? Frenos { get; set; }

    public virtual Vehiculo Vehicle { get; set; } = null!;
}
