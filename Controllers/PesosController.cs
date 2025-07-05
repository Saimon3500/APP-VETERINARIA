using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class PesosController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public PesosController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Pesos
        public async Task<IActionResult> Index()
        {
            var veterinariaExtendidaContext = _context.HistPesos.Include(h => h.CodMascotaNavigation);
            return View(await veterinariaExtendidaContext.ToListAsync());
        }

        // GET: Pesos/Details/5
        public async Task<IActionResult> Details(string codMascota, DateOnly fechaPesaje)
        {
            if (codMascota == null || fechaPesaje == default)
            {
                return NotFound();
            }

            var histPeso = await _context.HistPesos
                .Include(h => h.CodMascotaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.FechaPesaje == fechaPesaje);
            if (histPeso == null)
            {
                return NotFound();
            }

            return View(histPeso);
        }

        // GET: Pesos/Create
        public IActionResult Create()
        {
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota");
            return View();
        }

        // POST: Pesos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodMascota,FechaPesaje,Peso")] HistPeso histPeso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(histPeso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", histPeso.CodMascota);
            return View(histPeso);
        }

        // GET: Pesos/Edit/5
        public async Task<IActionResult> Edit(string codMascota, DateOnly fechaPesaje)
        {
            if (codMascota == null || fechaPesaje == default)
            {
                return NotFound();
            }

            var histPeso = await _context.HistPesos.FindAsync(codMascota, fechaPesaje);
            if (histPeso == null)
            {
                return NotFound();
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", histPeso.CodMascota);
            return View(histPeso);
        }

        // POST: Pesos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string codMascota, DateOnly fechaPesaje, [Bind("CodMascota,FechaPesaje,Peso")] HistPeso histPeso)
        {
            if (codMascota != histPeso.CodMascota || fechaPesaje != histPeso.FechaPesaje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(histPeso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistPesoExists(histPeso.CodMascota, histPeso.FechaPesaje))
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
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", histPeso.CodMascota);
            return View(histPeso);
        }

        // GET: Pesos/Delete/5
        public async Task<IActionResult> Delete(string codMascota, DateOnly fechaPesaje)
        {
            if (codMascota == null || fechaPesaje == default)
            {
                return NotFound();
            }

            var histPeso = await _context.HistPesos
                .Include(h => h.CodMascotaNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == codMascota && m.FechaPesaje == fechaPesaje);
            if (histPeso == null)
            {
                return NotFound();
            }

            return View(histPeso);
        }

        // POST: Pesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string codMascota, DateOnly fechaPesaje)
        {
            var histPeso = await _context.HistPesos.FindAsync(codMascota, fechaPesaje);
            if (histPeso != null)
            {
                _context.HistPesos.Remove(histPeso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistPesoExists(string codMascota, DateOnly fechaPesaje)
        {
            return _context.HistPesos.Any(e => e.CodMascota == codMascota && e.FechaPesaje == fechaPesaje);
        }
    }
}
