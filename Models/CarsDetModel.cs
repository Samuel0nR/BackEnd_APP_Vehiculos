using System;
using System.Collections.Generic;

namespace api_dotNet_vehicles.Models;

public partial class CarsDetModel
{
    public int Id { get; set; }
    public int CodVehi { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? CodTipo { get; set; }
    public string? Transmision { get; set; }
    public string? Motor { get; set; }
    public string? Frenos_Del { get; set; }
    public string? Frenos_Tras { get; set; }
    public string? Neumatico { get; set; }
    public string? Llanta { get; set; }
    public double? Km_Carr { get; set; }
    public double? Km_Ciu { get; set; }
    public double? Km_Mix { get; set; }
    public int? Peso_Neto_KG { get; set; }
    public int? Peso_Bruto_KG { get; set; }
}
