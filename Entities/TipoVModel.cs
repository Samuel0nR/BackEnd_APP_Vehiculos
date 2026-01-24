using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace api_dotNet_vehicles.Models;

[Keyless]
public partial class TipoVModel
{
    public string? Codigo { get; set; }

    public string? Tipo { get; set; }

    public int? CatV { get; set; }

}
