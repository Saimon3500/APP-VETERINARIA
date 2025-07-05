using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class Persona
{
    [Required]
    [StringLength(20, ErrorMessage = "El campo CI no puede exceder los 20 caracteres.")]
    public string Ci { get; set; }

    [Required]
    [StringLength(80, ErrorMessage = "El campo Nombre no puede exceder los 80 caracteres.")]
    public string Nombre { get; set; }

    [StringLength(50, ErrorMessage = "El campo Telefono no puede exceder los 50 caracteres.")]
    public string Telefono { get; set; }

    [StringLength(50, ErrorMessage = "El campo Correo no puede exceder los 50 caracteres.")]
    public string Correo { get; set; }

    [StringLength(60, ErrorMessage = "El campo Direccion no puede exceder los 60 caracteres.")]
    public string Direccion { get; set; }

    public virtual ICollection<PersonaCliente> PersonaClientes { get; set; } = new List<PersonaCliente>();
}
