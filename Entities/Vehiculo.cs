using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class Vehiculo
{
    public Guid VehicleId { get; set; }

    public string? Patente { get; set; }

    public int MarcaId { get; set; }

    public int ModeloId { get; set; }

    public int TipoId { get; set; }

    public int SubTipoId { get; set; }

    public int Anio { get; set; }

    public string? Color { get; set; }

    public virtual AutosDet? AutosDet { get; set; }

    public virtual BicicletasDet? BicicletasDet { get; set; }

    public virtual Modelo Modelo { get; set; } = null!;

    public virtual MotosDet? MotosDet { get; set; }

    public virtual SubTipoVehiculo SubTipo { get; set; } = null!;

    public virtual TipoVehiculo Tipo { get; set; } = null!;

    public virtual ICollection<UserVehicle> UserVehicles { get; set; } = [];
}
