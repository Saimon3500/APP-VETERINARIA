using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace VetApp.Models;

public partial class ConsumoHotel
{
    public int IdHospedaje { get; set; }

    public string IdServicio { get; set; } = null!;

    public string CodMascota { get; set; } = null!;

    public int? CodAlimento { get; set; }

    public int? CodMedicamento { get; set; }

    public int? IdComodidad { get; set; }

    public string Nit { get; set; } = null!;

    public string Observaciones { get; set; } = null!;
    public int NochesHosp { get; set; } 
    public int CantidadAlim { get; set; }

    public int CantidadMedic { get; set; }

    public int CantidadCom { get; set; }
    public int CantidadBanos { get; set; }
    [ValidateNever]
    public virtual Alimento? CodAlimentoNavigation { get; set; }
    [ValidateNever]
    public virtual Mascota CodMascotaNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual Medicamento? CodMedicamentoNavigation { get; set; }
    [ValidateNever]
    public virtual Comodidade? IdComodidadNavigation { get; set; }
    [ValidateNever]
    public virtual Hospedaje IdHospedajeNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
