using System;
using System.Collections.Generic;

namespace VetApp.Models;

public partial class Servicio
{
    public string IdServicio { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string UnidadMedida { get; set; } = null!;

    public string TipoServicio { get; set; } = null!;

    public virtual ICollection<ConsumoHotel> ConsumoHotels { get; set; } = new List<ConsumoHotel>();

    public virtual ICollection<ConsumosVet> ConsumosVets { get; set; } = new List<ConsumosVet>();
}
