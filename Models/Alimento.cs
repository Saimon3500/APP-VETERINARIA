using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class Alimento
{
    [Display(Name ="ID del alimento")]
    public int CodAlimento { get; set; }
    [Display(Name = "Nombre")]

    public string Nombre { get; set; } = null!;
    [Display(Name = "Descripción")]

    public string Descripcion { get; set; } = null!;

    public string Proveedor { get; set; } = null!;
    [Display(Name = "Precio unitario")]

    public decimal PrecioUnitario { get; set; }

    public virtual ICollection<ConsumoHotel> ConsumoHotels { get; set; } = new List<ConsumoHotel>();
}
