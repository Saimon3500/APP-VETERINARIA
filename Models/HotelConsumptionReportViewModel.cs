using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public class HotelConsumptionReportViewModel : IValidatableObject
    {
        public int IdHospedaje { get; set; }
        [ValidateNever]
        public string CodMascota { get; set; }
        [ValidateNever]
        public string NombreMascota { get; set; }
        [ValidateNever]
        public string Cliente { get; set; }
        [ValidateNever]
        public string IdServicio { get; set; }
        [ValidateNever]
        public string NombreServicio { get; set; }
        [ValidateNever]
        public string Observaciones { get; set; }
        public int NochesHosp { get; set; }
        public int CantidadAlim { get; set; }
        public int CantidadMedic { get; set; }
        public int CantidadCom { get; set; }
        public int CantidadBanos { get; set; }  // Nueva propiedad para la cantidad de baños
        [ValidateNever]
        public string Nit { get; set; }
        public DateOnly Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal? PrecioTotalGeneral { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [Display(Name = "Fecha Ingreso")]
        [DataType(DataType.Date)]
        public DateOnly FechaIngreso { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria")]
        [Display(Name = "Fecha Salida")]
        [DataType(DataType.Date)]
        public DateOnly FechaSalida { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaSalida < FechaIngreso)
            {
                yield return new ValidationResult(
                    "La fecha de salida no puede ser menor que la fecha de ingreso.",
                    new[] { nameof(FechaSalida) }
                );
            }
        }
    }
}
