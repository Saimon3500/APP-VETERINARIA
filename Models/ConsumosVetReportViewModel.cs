namespace VetApp.Models
{
    public class ConsumosVetReportViewModel
    {
        
        public string CodMascota { get; set; }
        public string NombreMascota { get; set; }
        public string Cliente { get; set; }
        public string IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Observaciones { get; set; }
        public int CantVacunas { get; set; }
        public string Nit { get; set; }
        public DateOnly Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
    }

}
