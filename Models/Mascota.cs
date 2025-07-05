using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetApp.Models;

public partial class Mascota
{
    [Required]
    [StringLength(20, ErrorMessage = "El campo CodMascota no puede exceder los 20 caracteres.")]
    public string CodMascota { get; set; } = null!;

    [Required]
    [StringLength(20, ErrorMessage = "El campo CodCliente no puede exceder los 20 caracteres.")]
    public string CodCliente { get; set; } = null!;

    [Required]
    [StringLength(80, ErrorMessage = "El campo Nombre no puede exceder los 80 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "El campo Especie no puede exceder los 30 caracteres.")]
    public string Especie { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "El campo Raza no puede exceder los 30 caracteres.")]
    public string Raza { get; set; } = null!;

    [Required]
    [StringLength(20, ErrorMessage = "El campo Color no puede exceder los 20 caracteres.")]
    public string Color { get; set; } = null!;

    public DateOnly? FechaNac { get; set; }

    public virtual ICollection<AplicaVacuna> AplicaVacunas { get; set; } = new List<AplicaVacuna>();

    [ValidateNever]
    public virtual Cliente CodClienteNavigation { get; set; } = null!;

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public virtual ICollection<ConsumoHotel> ConsumoHotels { get; set; } = new List<ConsumoHotel>();

    public virtual ICollection<ConsumosVet> ConsumosVets { get; set; } = new List<ConsumosVet>();

    public virtual ICollection<HistPeso> HistPesos { get; set; } = new List<HistPeso>();

    public virtual ICollection<Hospedaje> Hospedajes { get; set; } = new List<Hospedaje>();
}
