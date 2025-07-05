using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace VetApp.Controllers
{
    public class RegistrarBanoExtraController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public RegistrarBanoExtraController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegistrarBano()
        {
            var hospedajes = await _context.Hospedajes.ToListAsync();
            ViewBag.Hospedajes = new SelectList(hospedajes, "IdHospedaje", "IdHospedaje");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarBano(RegistrarBanoExtraViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                "EXEC RegistrarBanoExtra @IdHospedaje, @CantidadBanos",
                new SqlParameter("@IdHospedaje", model.IdHospedaje),
                new SqlParameter("@CantidadBanos", model.CantidadBanos)
                );

                ViewBag.Message = "Baño extra registrado con éxito.";
                return RedirectToAction("Index");
            }
            var hospedajes = await _context.Hospedajes.ToListAsync();
            ViewBag.Hospedajes = new SelectList(hospedajes, "IdHospedaje", "IdHospedaje");
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var banos = await _context.ConsumoHotels
                .Where(ch => ch.IdServicio == "BE001" || ch.IdServicio == "BE002" || ch.IdServicio == "BE003")
                .Select(ch => new RegistrarBanoExtraViewModel
                {
                    IdHospedaje = ch.IdHospedaje,
                    CantidadBanos = ch.CantidadBanos
                })
                .ToListAsync();

            return View(banos);
        }
        // GET: RegistrarBanoExtra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumoHotel = await _context.ConsumoHotels
                .FirstOrDefaultAsync(ch => ch.IdHospedaje == id && (ch.IdServicio == "BE001" || ch.IdServicio == "BE002" || ch.IdServicio == "BE003"));
            if (consumoHotel == null)
            {
                return NotFound();
            }

            var viewModel = new RegistrarBanoExtraViewModel
            {
                IdHospedaje = consumoHotel.IdHospedaje,
                CantidadBanos = consumoHotel.CantidadBanos
            };

            return View(viewModel);
        }

        // POST: RegistrarBanoExtra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RegistrarBanoExtraViewModel model)
        {
            if (id != model.IdHospedaje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC UpdateBanoCount @idHospedaje = {0}, @cantidadBanos = {1}",
                        model.IdHospedaje, model.CantidadBanos
                    );

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(model);
        }

        // GET: RegistrarBanoExtra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumoHotel = await _context.ConsumoHotels
                .FirstOrDefaultAsync(ch => ch.IdHospedaje == id && (ch.IdServicio == "BE001" || ch.IdServicio == "BE002" || ch.IdServicio == "BE003"));
            if (consumoHotel == null)
            {
                return NotFound();
            }

            var viewModel = new RegistrarBanoExtraViewModel
            {
                IdHospedaje = consumoHotel.IdHospedaje,
                CantidadBanos = consumoHotel.CantidadBanos
            };

            return View(viewModel);
        }

        // POST: RegistrarBanoExtra/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteBano @idHospedaje = {0}",
                    id
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                var consumoHotel = await _context.ConsumoHotels
                    .FirstOrDefaultAsync(ch => ch.IdHospedaje == id && (ch.IdServicio == "BE001" || ch.IdServicio == "BE002" || ch.IdServicio == "BE003"));
                var viewModel = new RegistrarBanoExtraViewModel
                {
                    IdHospedaje = consumoHotel.IdHospedaje,
                    CantidadBanos = consumoHotel.CantidadBanos
                };

                return View(viewModel);
            }
        }
    }
}
