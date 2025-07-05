using System;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public class ConsumosVetCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        [Display(Name = "Fecha Fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El código de la mascota es obligatorio")]
        [Display(Name = "Código de Mascota")]
        [StringLength(20, ErrorMessage = "El código de la mascota no puede exceder los 20 caracteres.")]
        public string CodMascota { get; set; } = null!;

        [Display(Name = "Observaciones")]
        [StringLength(200, ErrorMessage = "Las observaciones no pueden exceder los 200 caracteres.")]
        public string Observaciones { get; set; } = null!;

        [Required(ErrorMessage = "El NIT es obligatorio")]
        [Display(Name = "NIT")]
        [StringLength(20, ErrorMessage = "El NIT no puede exceder los 20 caracteres.")]
        public string Nit { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaFin < FechaInicio)
            {
                yield return new ValidationResult(
                    "La fecha de fin no puede ser menor que la fecha de inicio",
                    new[] { nameof(FechaFin) }
                );
            }
        }
    }
}
