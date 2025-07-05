using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class ConsumosVet
{
    [Display(Name = "Mascota")]
    [StringLength(20, ErrorMessage = "El código de la mascota no puede exceder los 20 caracteres.")]
    public string CodMascota { get; set; } = null!;

    [Display(Name = "Vacuna aplicada")]
    [StringLength(20, ErrorMessage = "El código de la vacuna no puede exceder los 20 caracteres.")]
    public string? CodVacuna { get; set; }

    [Display(Name = "Servicio utilizado")]
    [StringLength(20, ErrorMessage = "El ID del servicio no puede exceder los 20 caracteres.")]
    public string IdServicio { get; set; } = null!;

    [Display(Name = "ID Consumo Médico")]
    public int IdConsumoVet { get; set; }

    [StringLength(200, ErrorMessage = "Las observaciones no pueden exceder los 200 caracteres.")]
    public string Observaciones { get; set; } = null!;

    [Display(Name = "Vacunas Aplicadas")]
    public int CantVacunas { get; set; }

    [StringLength(20, ErrorMessage = "El NIT no puede exceder los 20 caracteres.")]
    public string Nit { get; set; } = null!;

    [ValidateNever]
    public virtual Mascota CodMascotaNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Vacuna? CodVacunaNavigation { get; set; }

    [ValidateNever]
    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
