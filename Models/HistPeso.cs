using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public partial class HistPeso
    {
        [Display(Name = "Mascota")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CodMascota { get; set; } = null!;

        [Display(Name = "Fecha del pesaje")]
        [CustomValidation(typeof(HistPeso), nameof(ValidateFechaPesaje))]
        public DateOnly FechaPesaje { get; set; }

        [Display(Name = "Peso a la fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El peso debe ser mayor a 0.")]
        public decimal Peso { get; set; }

        [ValidateNever]
        [Display(Name = "Mascota")]
        public virtual Mascota CodMascotaNavigation { get; set; } = null!;

        public static ValidationResult? ValidateFechaPesaje(DateOnly fechaPesaje, ValidationContext context)
        {
            if (fechaPesaje > DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult("La fecha del pesaje no puede ser mayor a la fecha de hoy.");
            }
            return ValidationResult.Success;
        }
    }
}
