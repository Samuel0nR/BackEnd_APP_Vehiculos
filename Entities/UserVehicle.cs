using System;
using System.Collections.Generic;

namespace API_VehiclesAPP.Entities;

public partial class UserVehicle
{
    public Guid UserId { get; set; }

    public Guid VehicleId { get; set; }

    public string? AliasVehiculo { get; set; }

    public bool IsOwner { get; set; }

    public bool Favorito { get; set; }

    public virtual Vehiculo Vehicle { get; set; } = null!;
}
