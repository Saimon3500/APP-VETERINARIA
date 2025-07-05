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
    public class HotelReportController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public HotelReportController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        public IActionResult GenerateHotelReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateHotelReport(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var reportResults = new List<HotelConsumptionReportViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GenerateHotelConsumptionReport";
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
                        reportResults.Add(new HotelConsumptionReportViewModel
                        {
                            CodMascota = result.GetString(0),
                            NombreMascota = result.GetString(1),
                            Cliente = result.GetString(2),
                            IdServicio = result.GetString(3),
                            NombreServicio = result.GetString(4),
                            Observaciones = result.GetString(5),
                            NochesHosp = result.GetInt32(6),
                            CantidadAlim = result.GetInt32(7),
                            CantidadMedic = result.GetInt32(8),
                            CantidadCom = result.GetInt32(9),
                            CantidadBanos = result.GetInt32(10),  // Nueva propiedad para la cantidad de baños
                            Nit = result.GetString(11),
                            Fecha = DateOnly.FromDateTime(result.GetDateTime(12)),
                            PrecioTotal = result.GetDecimal(13),
                            PrecioTotalGeneral = result.IsDBNull(14) ? (decimal?)null : result.GetDecimal(14)
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

            return View("HotelReportResults", reportResults);
        }
            
        }
    }
