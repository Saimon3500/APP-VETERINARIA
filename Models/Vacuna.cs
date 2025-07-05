using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public partial class Vacuna
    {
        [Required]
        [StringLength(20, ErrorMessage = "El campo CodVacuna no puede exceder los 20 caracteres.")]
        public string CodVacuna { get; set; } = null!;

        [Required]
        [Display(Name ="Nombre de la vacuna")]
        [StringLength(80, ErrorMessage = "El campo Nombre no puede exceder los 80 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(80, ErrorMessage = "El campo Laboratorio no puede exceder los 80 caracteres.")]
        public string Laboratorio { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "El campo PrevEnfermedad no puede exceder los 50 caracteres.")]
        public string PrevEnfermedad { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo Dosis debe ser mayor que 0.")]
        public decimal Dosis { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo PrecioUnitario debe ser mayor que 0.")]
        public decimal PrecioUnitario { get; set; }

        public virtual ICollection<AplicaVacuna> AplicaVacunas { get; set; } = new List<AplicaVacuna>();

        public virtual ICollection<ConsumosVet> ConsumosVets { get; set; } = new List<ConsumosVet>();
    }
}
