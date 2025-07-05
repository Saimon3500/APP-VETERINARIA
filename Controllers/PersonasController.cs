using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class PersonasController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public PersonasController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personas.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Ci == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ci,Nombre,Telefono,Correo,Direccion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.InsertPersona(persona.Ci, persona.Nombre, persona.Telefono, persona.Correo, persona.Direccion);
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex) when (ex.Number == 50000)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ci,Nombre,Telefono,Correo,Direccion")] Persona persona)
        {
            if (id != persona.Ci)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdatePersona(persona.Ci, persona.Nombre, persona.Telefono, persona.Correo, persona.Direccion);
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex) when (ex.Number == 50000)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Ci == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                _context.DeletePersona(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return View();
            }
        }

        private bool PersonaExists(string id)
        {
            return _context.Personas.Any(e => e.Ci == id);
        }
    }
}
