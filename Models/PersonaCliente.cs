using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace VetApp.Models;

public partial class PersonaCliente
{
    public string CodCliente { get; set; } = null!;

    public string Ci { get; set; } = null!;

    public DateOnly FechaAsociacion { get; set; }
    [ValidateNever]
    public virtual Persona CiNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual Cliente CodClienteNavigation { get; set; } = null!;
}
