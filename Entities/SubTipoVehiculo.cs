using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class SubTipoVehiculo
{
    public int SubTipoId { get; set; }

    public int TipoId { get; set; }

    public string SubTipo { get; set; } = null!;

    public virtual TipoVehiculo Tipo { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = [];
}
