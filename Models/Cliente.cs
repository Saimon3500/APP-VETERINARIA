using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models
{
    public partial class Cliente
    {
        [Key]
        [StringLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string CodCliente { get; set; } = null!;

        [Required]
        [StringLength(80, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Apellido { get; set; } = null!;

        [Required]
        [StringLength(40, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string CuentaBanco { get; set; } = null!;

        [Required]
        [StringLength(80, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Banco { get; set; } = null!;

        [Required]
        [StringLength(60, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Direccion { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Telefono { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Correo { get; set; } = null!;

        public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

        public virtual ICollection<PersonaCliente> PersonaClientes { get; set; } = new List<PersonaCliente>();
    }
}
