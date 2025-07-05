using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class VacunasController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public VacunasController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Vacunas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vacunas.ToListAsync());
        }

        // GET: Vacunas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.CodVacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // GET: Vacunas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacunas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodVacuna,Nombre,Laboratorio,PrevEnfermedad,Dosis,PrecioUnitario")] Vacuna vacuna)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.InsertVacuna(vacuna.CodVacuna, vacuna.Nombre, vacuna.Laboratorio, vacuna.PrevEnfermedad, vacuna.Dosis, vacuna.PrecioUnitario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex) when (ex.Number == 50000)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                }
            }
            return View(vacuna);
        }

        // GET: Vacunas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null)
            {
                return NotFound();
            }
            return View(vacuna);
        }

        // POST: Vacunas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodVacuna,Nombre,Laboratorio,PrevEnfermedad,Dosis,PrecioUnitario")] Vacuna vacuna)
        {
            if (id != vacuna.CodVacuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdateVacuna(vacuna.CodVacuna, vacuna.Nombre, vacuna.Laboratorio, vacuna.PrevEnfermedad, vacuna.Dosis, vacuna.PrecioUnitario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex) when (ex.Number == 50000)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                }
            }
            return View(vacuna);
        }

        // GET: Vacunas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.CodVacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // POST: Vacunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                _context.DeleteVacuna(id);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }

        private bool VacunaExists(string id)
        {
            return _context.Vacunas.Any(e => e.CodVacuna == id);
        }
    }
}
