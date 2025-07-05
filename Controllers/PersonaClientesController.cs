using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class PersonaClientesController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public PersonaClientesController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: PersonaClientes
        public async Task<IActionResult> Index()
        {
            var veterinariaExtendidaContext = _context.PersonaClientes.Include(p => p.CiNavigation).Include(p => p.CodClienteNavigation);
            return View(await veterinariaExtendidaContext.ToListAsync());
        }

        // GET: PersonaClientes/Details
        public async Task<IActionResult> Details(string codCliente, string ci, DateOnly fechaAsociacion)
        {
            if (codCliente == null || ci == null)
            {
                return NotFound();
            }

            var personaCliente = await _context.PersonaClientes
                .Include(p => p.CiNavigation)
                .Include(p => p.CodClienteNavigation)
                .FirstOrDefaultAsync(m => m.CodCliente == codCliente && m.Ci == ci && m.FechaAsociacion == fechaAsociacion);
            if (personaCliente == null)
            {
                return NotFound();
            }

            return View(personaCliente);
        }

        // GET: PersonaClientes/Create
        public IActionResult Create()
        {
            ViewData["Ci"] = new SelectList(_context.Personas, "Ci", "Ci");
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente");
            return View();
        }

        // POST: PersonaClientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCliente,Ci,FechaAsociacion")] PersonaCliente personaCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.InsertPersonaCliente(personaCliente.CodCliente, personaCliente.Ci, personaCliente.FechaAsociacion);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            ViewData["Ci"] = new SelectList(_context.Personas, "Ci", "Ci", personaCliente.Ci);
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", personaCliente.CodCliente);
            return View(personaCliente);
        }

        // GET: PersonaClientes/Edit
        public async Task<IActionResult> Edit(string codCliente, string ci, DateOnly fechaAsociacion)
        {
            if (codCliente == null || ci == null)
            {
                return NotFound();
            }

            var personaCliente = await _context.PersonaClientes.FindAsync(codCliente, ci, fechaAsociacion);
            if (personaCliente == null)
            {
                return NotFound();
            }
            ViewData["Ci"] = new SelectList(_context.Personas, "Ci", "Ci", personaCliente.Ci);
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", personaCliente.CodCliente);
            return View(personaCliente);
        }

        // POST: PersonaClientes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string codCliente, string ci, DateOnly fechaAsociacion, [Bind("CodCliente,Ci,FechaAsociacion")] PersonaCliente personaCliente)
        {
            if (codCliente != personaCliente.CodCliente || ci != personaCliente.Ci || fechaAsociacion != personaCliente.FechaAsociacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdatePersonaCliente(personaCliente.CodCliente, personaCliente.Ci, personaCliente.FechaAsociacion);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            ViewData["Ci"] = new SelectList(_context.Personas, "Ci", "Ci", personaCliente.Ci);
            ViewData["CodCliente"] = new SelectList(_context.Clientes, "CodCliente", "CodCliente", personaCliente.CodCliente);
            return View(personaCliente);
        }

        // GET: PersonaClientes/Delete
        public async Task<IActionResult> Delete(string codCliente, string ci, DateOnly fechaAsociacion)
        {
            if (codCliente == null || ci == null)
            {
                return NotFound();
            }

            var personaCliente = await _context.PersonaClientes
                .Include(p => p.CiNavigation)
                .Include(p => p.CodClienteNavigation)
                .FirstOrDefaultAsync(m => m.CodCliente == codCliente && m.Ci == ci && m.FechaAsociacion == fechaAsociacion);
            if (personaCliente == null)
            {
                return NotFound();
            }

            return View(personaCliente);
        }

        // POST: PersonaClientes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string codCliente, string ci)
        {
            try
            {
                _context.DeletePersonaCliente(codCliente, ci);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View();
            }
        }

        private bool PersonaClienteExists(string codCliente, string ci, DateOnly fechaAsociacion)
        {
            return _context.PersonaClientes.Any(e => e.CodCliente == codCliente && e.Ci == ci && e.FechaAsociacion == fechaAsociacion);
        }
    }
}
