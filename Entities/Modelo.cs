using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class Modelo
{
    public int ModeloId { get; set; }

    public int MarcaId { get; set; }

    public int TipoVehiculo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual TipoVehiculo TipoVehiculoNavigation { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = [];
}
