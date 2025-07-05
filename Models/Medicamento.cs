using System;
using System.Collections.Generic;

namespace VetApp.Models;

public partial class Medicamento
{
    public int CodMedicamento { get; set; }

    public string Laboratorio { get; set; } = null!;

    public string Presentacion { get; set; } = null!;

    public decimal PesoNeto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ConsumoHotel> ConsumoHotels { get; set; } = new List<ConsumoHotel>();
}
