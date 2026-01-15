using System;
using System.Collections.Generic;

namespace api_dotNet_vehicles.Models;

public partial class BikesDetModel
{
    public int Id { get; set; }
    public int CodVehi { get; set; }

    public string Marca { get; set; } = null!;
    public string Modelo { get; set; } = null!;
    public string CodTipo { get; set; } = null!;
    public int? Velocidades { get; set; }
    public string? Suspension { get; set; }
    public string? Frenos { get; set; }
    public string? Cuadro { get; set; }
    public string? Llanta { get; set; }
    public string? Valvula { get; set; }
    public string? Peso { get; set; }
}
