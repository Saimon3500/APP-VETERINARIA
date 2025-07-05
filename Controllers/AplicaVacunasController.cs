using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class AplicaVacunasController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public AplicaVacunasController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: AplicaVacunas
        public async Task<IActionResult> Index()
        {
            var aplicaVacunas = _context.AplicaVacunas
                .Include(av => av.CodMascotaNavigation)
                .Include(av => av.CodVacunaNavigation);
            return View(await aplicaVacunas.ToListAsync());
        }

        // GET: AplicaVacunas/Details
        public async Task<IActionResult> Details(string codMascota, string codVacuna, DateOnly fechaPrevista)
        {
            if (codMascota == null || codVacuna == null)
            {
                return NotFound();
            }

            var aplicaVacuna = await _context.AplicaVacunas
                .Include(av => av.CodMascotaNavigation)
                .Include(av => av.CodVacunaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.CodVacuna == codVacuna && m.FechaPrevista == fechaPrevista);
            if (aplicaVacuna == null)
            {
                return NotFound();
            }

            return View(aplicaVacuna);
        }

        // GET: AplicaVacunas/Create
        public IActionResult Create()
        {
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota");
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna");
            return View();
        }

        // POST: AplicaVacunas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodMascota,CodVacuna,FechaPrevista,FechaAplicacion,DosisAplicada")] AplicaVacuna aplicaVacuna)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (aplicaVacuna.FechaPrevista < DateOnly.FromDateTime(DateTime.Now))
                    {
                        ModelState.AddModelError("FechaPrevista", "La Fecha Prevista no puede ser anterior a la fecha de hoy.");
                        ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", aplicaVacuna.CodMascota);
                        ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna", aplicaVacuna.CodVacuna);
                        return View(aplicaVacuna);
                    }

                    _context.Add(aplicaVacuna);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la aplicación de vacuna: " + ex.Message);
                }
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", aplicaVacuna.CodMascota);
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna", aplicaVacuna.CodVacuna);
            return View(aplicaVacuna);
        }

        // GET: AplicaVacunas/Edit
        public async Task<IActionResult> Edit(string codMascota, string codVacuna, DateOnly fechaPrevista)
        {
            if (codMascota == null || codVacuna == null)
            {
                return NotFound();
            }

            var aplicaVacuna = await _context.AplicaVacunas.FindAsync(codMascota, codVacuna, fechaPrevista);
            if (aplicaVacuna == null)
            {
                return NotFound();
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", aplicaVacuna.CodMascota);
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna", aplicaVacuna.CodVacuna);
            return View(aplicaVacuna);
        }

        // POST: AplicaVacunas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string codMascota, string codVacuna, DateOnly fechaPrevista, [Bind("CodMascota,CodVacuna,FechaPrevista,FechaAplicacion,DosisAplicada")] AplicaVacuna aplicaVacuna)
        {
            if (codMascota != aplicaVacuna.CodMascota || codVacuna != aplicaVacuna.CodVacuna || fechaPrevista != aplicaVacuna.FechaPrevista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aplicaVacuna.FechaPrevista < DateOnly.FromDateTime(DateTime.Now))
                    {
                        ModelState.AddModelError("FechaPrevista", "La Fecha Prevista no puede ser anterior a la fecha de hoy.");
                        ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", aplicaVacuna.CodMascota);
                        ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna", aplicaVacuna.CodVacuna);
                        return View(aplicaVacuna);
                    }

                    _context.Update(aplicaVacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicaVacunaExists(aplicaVacuna.CodMascota, aplicaVacuna.CodVacuna, aplicaVacuna.FechaPrevista))
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
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", aplicaVacuna.CodMascota);
            ViewData["CodVacuna"] = new SelectList(_context.Vacunas, "CodVacuna", "CodVacuna", aplicaVacuna.CodVacuna);
            return View(aplicaVacuna);
        }

        // GET: AplicaVacunas/Delete
        public async Task<IActionResult> Delete(string codMascota, string codVacuna, DateOnly fechaPrevista)
        {
            if (codMascota == null || codVacuna == null)
            {
                return NotFound();
            }

            var aplicaVacuna = await _context.AplicaVacunas
                .Include(av => av.CodMascotaNavigation)
                .Include(av => av.CodVacunaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.CodVacuna == codVacuna && m.FechaPrevista == fechaPrevista);
            if (aplicaVacuna == null)
            {
                return NotFound();
            }

            return View(aplicaVacuna);
        }

        // POST: AplicaVacunas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string codMascota, string codVacuna, DateOnly fechaPrevista)
        {
            var aplicaVacuna = await _context.AplicaVacunas.FindAsync(codMascota, codVacuna, fechaPrevista);
            if (aplicaVacuna != null)
            {
                _context.AplicaVacunas.Remove(aplicaVacuna);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AplicaVacunaExists(string codMascota, string codVacuna, DateOnly fechaPrevista)
        {
            return _context.AplicaVacunas.Any(e => e.CodMascota == codMascota && e.CodVacuna == codVacuna && e.FechaPrevista == fechaPrevista);
        }
    }
}
