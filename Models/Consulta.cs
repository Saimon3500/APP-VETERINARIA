using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class Consulta
{
    [Required]
    [StringLength(20)]
    public string CodMascota { get; set; } = null!;

    [Required]
    public DateOnly FechaConsulta { get; set; }

    [Required]
    [StringLength(50)]
    public string Motivo { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Diagnostico { get; set; } = null!;

    [Required]
    [StringLength(150)]
    public string Tratamiento { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Medicacion { get; set; } = null!;

    [ValidateNever]
    public virtual Mascota CodMascotaNavigation { get; set; } = null!;
}
