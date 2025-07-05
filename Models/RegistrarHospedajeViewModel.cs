using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace VetApp.ViewModels
{
    public class RegistrarHospedajeViewModel
    {
        [Display(Name = "Mascota")]
        public string CodMascota { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateOnly FechaIngreso { get; set; }

        [Display(Name = "Fecha de Salida")]
        [DataType(DataType.Date)]
        [GreaterThanOrEqualTo("FechaIngreso", ErrorMessage = "La fecha de salida no puede ser menor que la fecha de ingreso.")]
        public DateOnly FechaSalida { get; set; }

        [Display(Name = "Raza")]
        public string TamanoMascota { get; set; }

        [Display(Name = "Usa Necesidades Especiales")]
        public bool UsaNecesidadesEspeciales { get; set; }

        // Necesidades Especiales
        [Display(Name = "Nombre del alimento")]
        [ValidateNever]
        public string NombreAlimento { get; set; }
        [Display(Name = "Descripción del alimento")]
        [ValidateNever]
        public string DescripcionAlimento { get; set; }
        [Display(Name = "Proveedor")]
        [ValidateNever]
        public string ProveedorAlimento { get; set; }
        [Display(Name = "Cantidad")]
        public int CantidadAlimento { get; set; }
        [Display(Name = "Comodidad")]
        [ValidateNever]
        public string NombreComodidad { get; set; }
        [Display(Name = "Descripción")]
        [ValidateNever]
        public string DescripcionComodidad { get; set; }
        [Display(Name = "Cantidad")]
        public int CantidadComodidad { get; set; }
        [Display(Name = "Nombre del medicamento")]
        [ValidateNever]
        public string NombreMedicamento { get; set; }
        [Display(Name = "Laboratorio")]
        [ValidateNever]
        public string LaboratorioMedicamento { get; set; }
        [Display(Name = "Presentación")]
        [ValidateNever]
        public string PresentacionMedicamento { get; set; }
        [Display(Name = "Peso neto")]
        public decimal PesoNetoMedicamento { get; set; }
        [Display(Name = "Cantidad")]
        public int CantidadMedicamento { get; set; }

        // Propiedad de navegación
        [ValidateNever]
        public SelectList Mascotas { get; set; }
    }

    public class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanOrEqualToAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateOnly)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateOnly)property.GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
