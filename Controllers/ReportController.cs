using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public ReportController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        public IActionResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateOnly fechaInicio, DateOnly fechaFin)
        {

            if (fechaFin < fechaInicio)
            {
                ModelState.AddModelError("fechaFin", "La fecha de fin no puede ser menor que la fecha de inicio.");
                return View();
            }

            var reportResults = new List<ConsumosVetReportViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GenerateReportWithTotalPrice";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                var resultadoParam = new SqlParameter("@resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(resultadoParam);

                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        reportResults.Add(new ConsumosVetReportViewModel
                        {
                            CodMascota = result.GetString(0),
                            NombreMascota = result.GetString(1),
                            Cliente = result.GetString(2),
                            IdServicio = result.GetString(3),
                            NombreServicio = result.GetString(4),
                            Observaciones = result.GetString(5),
                            CantVacunas = result.GetInt32(6),
                            Nit = result.GetString(7),
                            Fecha = DateOnly.FromDateTime(result.GetDateTime(8)),
                            PrecioTotal = result.GetDecimal(9)
                        });
                    }
                }
                

                await _context.Database.CloseConnectionAsync();
                // Obtén el valor del parámetro de salida
                var resultado = (int)resultadoParam.Value;
                if (resultado != 1)
                {
                    ModelState.AddModelError("", "Error generating report.");
                    return View();
                }
            }

            return View("ReportResults", reportResults);
        }

    }
}
