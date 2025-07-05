using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public ConsultasController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var veterinariaExtendidaContext = _context.Consultas.Include(c => c.CodMascotaNavigation);
            return View(await veterinariaExtendidaContext.ToListAsync());
        }

        // GET: Consultas/Details
        public async Task<IActionResult> Details(string codMascota, string fechaConsulta)
        {
            if (codMascota == null || fechaConsulta == null)
            {
                return NotFound();
            }

            if (!DateOnly.TryParse(fechaConsulta, out var parsedFechaConsulta))
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.CodMascotaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.FechaConsulta == parsedFechaConsulta);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota");
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodMascota,FechaConsulta,Motivo,Diagnostico,Tratamiento,Medicacion")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                await _context.InsertarConsulta(consulta.CodMascota, consulta.FechaConsulta, consulta.Motivo, consulta.Diagnostico, consulta.Tratamiento, consulta.Medicacion);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", consulta.CodMascota);
            return View(consulta);
        }

        // GET: Consultas/Edit
        public async Task<IActionResult> Edit(string codMascota, string fechaConsulta)
        {
            if (codMascota == null || fechaConsulta == null)
            {
                return NotFound();
            }

            if (!DateOnly.TryParse(fechaConsulta, out var parsedFechaConsulta))
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(codMascota, parsedFechaConsulta);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", consulta.CodMascota);
            return View(consulta);
        }

        // POST: Consultas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string codMascota, string fechaConsulta, [Bind("CodMascota,FechaConsulta,Motivo,Diagnostico,Tratamiento,Medicacion")] Consulta consulta)
        {
            if (!DateOnly.TryParse(fechaConsulta, out var parsedFechaConsulta))
            {
                return NotFound();
            }

            if (codMascota != consulta.CodMascota || parsedFechaConsulta != consulta.FechaConsulta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.ActualizarConsulta(consulta.CodMascota, consulta.FechaConsulta, consulta.Motivo, consulta.Diagnostico, consulta.Tratamiento, consulta.Medicacion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.CodMascota, consulta.FechaConsulta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", consulta.CodMascota);
            return View(consulta);
        }

        // GET: Consultas/Delete
        public async Task<IActionResult> Delete(string codMascota, string fechaConsulta)
        {
            if (codMascota == null || fechaConsulta == null)
            {
                return NotFound();
            }

            if (!DateOnly.TryParse(fechaConsulta, out var parsedFechaConsulta))
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.CodMascotaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.FechaConsulta == parsedFechaConsulta);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string codMascota, string fechaConsulta)
        {
            if (!DateOnly.TryParse(fechaConsulta, out var parsedFechaConsulta))
            {
                return NotFound();
            }

            await _context.BorrarConsulta(codMascota, parsedFechaConsulta);
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(string codMascota, DateOnly fechaConsulta)
        {
            return _context.Consultas.Any(e => e.CodMascota == codMascota && e.FechaConsulta == fechaConsulta);
        }
    }
}
