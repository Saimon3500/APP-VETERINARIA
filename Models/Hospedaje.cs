using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class Hospedaje
{
    [Display(Name ="ID")]
    public int IdHospedaje { get; set; }
    [Display(Name ="Mascota")]
    public string CodMascota { get; set; } = null!;
    [Display(Name = "Fecha de ingreso")]
    public DateOnly FechaIngreso { get; set; }
    [Display(Name = "Fecha de salida")]

    public DateOnly FechaSalida { get; set; }

    public string Observaciones { get; set; } = null!;

    public virtual ICollection<ConsumoHotel> ConsumoHotels { get; set; } = new List<ConsumoHotel>();
    [ValidateNever]
    [Display(Name ="Mascota")]
    public virtual Mascota CodMascotaNavigation { get; set; } = null!;
}
