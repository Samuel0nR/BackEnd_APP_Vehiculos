using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class TipoVehiculo
{
    public int TipoId { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = [];

    public virtual ICollection<SubTipoVehiculo> SubTipoVehiculos { get; set; } = [];

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = [];
}
