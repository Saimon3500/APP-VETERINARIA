using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class MascotasController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public MascotasController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            var mascotas = _context.Mascotas.Include(m => m.CodClienteNavigation);
            return View(await mascotas.ToListAsync());
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.CodClienteNavigation)
                .FirstOrDefaultAsync(m => m.CodMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create()
        {
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente");
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodMascota,CodCliente,Nombre,Especie,Raza,Color,FechaNac")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fechaNac = mascota.FechaNac.HasValue ? mascota.FechaNac.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
                    _context.InsertMascota(mascota.CodMascota, mascota.CodCliente, mascota.Nombre, mascota.Especie, mascota.Raza, mascota.Color, fechaNac);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null && sqlException.Number == 50000)
                    {
                        ModelState.AddModelError(string.Empty, sqlException.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                    }
                }
            }
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", mascota.CodCliente);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", mascota.CodCliente);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodMascota,CodCliente,Nombre,Especie,Raza,Color,FechaNac")] Mascota mascota)
        {
            if (id != mascota.CodMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fechaNac = mascota.FechaNac.HasValue ? mascota.FechaNac.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
                    _context.UpdateMascota(mascota.CodMascota, mascota.CodCliente, mascota.Nombre, mascota.Especie, mascota.Raza, mascota.Color, fechaNac);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null && sqlException.Number == 50000)
                    {
                        ModelState.AddModelError(string.Empty, sqlException.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                    }
                }
            }
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", mascota.CodCliente);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas

                .FirstOrDefaultAsync(m => m.CodMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (HasRelatedRecords(id))
            {
                ModelState.AddModelError(string.Empty, "No se puede eliminar la mascota ya que tiene registros asociados.");
                var mascota = await _context.Mascotas
                    .FirstOrDefaultAsync(m => m.CodMascota == id);
                return View(mascota);
            }

            try
            {
                _context.DeleteMascota(id);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                var mascota = await _context.Mascotas

                    .FirstOrDefaultAsync(m => m.CodMascota == id);
                return View(mascota);
            }
        }

        private bool MascotaExists(string id)
        {
            return _context.Mascotas.Any(e => e.CodMascota == id);
        }

        private bool HasRelatedRecords(string id)
        {
            return _context.ConsumoHotels.Any(ch => ch.CodMascota == id) || _context.ConsumosVets.Any(cv => cv.CodMascota == id);
        }
    }
}
