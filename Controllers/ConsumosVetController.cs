using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class ConsumosVetController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public ConsumosVetController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: ConsumosVet
        public async Task<IActionResult> Index()
        {
            var consumosVets = await _context.ConsumosVets
                .Include(c => c.CodMascotaNavigation)
                .Include(c => c.CodVacunaNavigation)
                .Include(c => c.IdServicioNavigation)
                .ToListAsync();
            return View(consumosVets);
        }

        // GET: ConsumosVet/Create
        public IActionResult Create()
        {
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "Nombre");
            return View();
        }

        // POST: ConsumosVet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateOnly FechaInicio, DateOnly FechaFin, string CodMascota, string Observaciones, string Nit, ConsumosVetCreateViewModel model)
        {
            if (model.FechaFin < model.FechaInicio)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser menor que la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC InsertarConsumoVet {FechaInicio}, {FechaFin}, {CodMascota}, {Observaciones}, {Nit}");


               

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "Nombre", CodMascota);
            return View();
        }

        // GET: ConsumosVet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumosVet = await _context.ConsumosVets
                .Include(c => c.CodMascotaNavigation)
                .Include(c => c.CodVacunaNavigation)
                .Include(c => c.IdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdConsumoVet == id);

            if (consumosVet == null)
            {
                return NotFound();
            }

            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "Nombre", consumosVet.CodMascota);
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "Nombre", consumosVet.CodVacuna);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "Nombre", consumosVet.IdServicio);
            return View(consumosVet);
        }

        // POST: ConsumosVet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsumoVet,CodMascota,CodVacuna,IdServicio,Observaciones,CantVacunas,Nit")] ConsumosVet consumosVet)
        {
            if (id != consumosVet.IdConsumoVet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumosVet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumosVetExists(consumosVet.IdConsumoVet))
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

            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "Nombre", consumosVet.CodMascota);
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "Nombre", consumosVet.CodVacuna);
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "Nombre", consumosVet.IdServicio);
            return View(consumosVet);
        }
        private bool ConsumosVetExists(int id)
        {
            return _context.ConsumosVets.Any(e => e.IdConsumoVet == id);
        }
    }

}
