using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public partial class AplicaVacuna
    {
        [Display(Name = "Código de la mascota")]
        public string CodMascota { get; set; } = null!;
        [Display(Name = "Código de la vacuna")]
        public string CodVacuna { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Prevista")]
        [Required(ErrorMessage = "La Fecha Prevista es obligatoria.")]
        [CustomValidation(typeof(AplicaVacuna), "ValidateFechaPrevista")]
        public DateOnly FechaPrevista { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Aplicación")]
        public DateOnly? FechaAplicacion { get; set; }
        [Display(Name ="Dosis aplicada")]
        public int DosisAplicada { get; set; }

        [ValidateNever]
        [Display(Name = "Código de la mascota")]
        public virtual Mascota CodMascotaNavigation { get; set; } = null!;

        [ValidateNever]
        [Display(Name = "Código de la vacuna")]
        public virtual Vacuna CodVacunaNavigation { get; set; } = null!;

        public static ValidationResult? ValidateFechaPrevista(DateOnly fechaPrevista, ValidationContext context)
        {
            if (fechaPrevista < DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("La Fecha Prevista no puede ser anterior a la fecha de hoy.");
            }
            return ValidationResult.Success;
        }
    }
}
