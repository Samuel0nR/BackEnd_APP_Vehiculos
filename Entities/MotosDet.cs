using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class MotosDet
{
    public Guid VehicleId { get; set; }

    public long? Kilometraje { get; set; }

    public int? Cilindrada { get; set; }

    public string? Transmision { get; set; }

    public string? Frenos { get; set; }

    public string? TipoEnfriamiento { get; set; }

    public virtual Vehiculo Vehicle { get; set; } = null!;
}
