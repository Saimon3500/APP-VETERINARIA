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
    public class HospedajesController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public HospedajesController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Hospedajes
        public async Task<IActionResult> Index()
        {
            var veterinariaExtendidaContext = _context.Hospedajes.Include(h => h.CodMascotaNavigation);
            return View(await veterinariaExtendidaContext.ToListAsync());
        }

     

        // GET: Hospedajes/Edit/5
        public async Task<IActionResult> Edit(int? id, string codMascota)
        {
            if (id == null || codMascota == null)
            {
                return NotFound();
            }

            var hospedaje = await _context.Hospedajes.FindAsync(id, codMascota);
            if (hospedaje == null)
            {
                return NotFound();
            }
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", hospedaje.CodMascota);
            return View(hospedaje);
        }

        // POST: Hospedajes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string codMascota, [Bind("IdHospedaje,CodMascota,FechaIngreso,FechaSalida,Observaciones")] Hospedaje hospedaje)
        {
            if (id != hospedaje.IdHospedaje || codMascota != hospedaje.CodMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospedaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospedajeExists(hospedaje.IdHospedaje, hospedaje.CodMascota))
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
            ViewData["CodMascota"] = new SelectList(_context.Mascotas, "CodMascota", "CodMascota", hospedaje.CodMascota);
            return View(hospedaje);
        }

        

        private bool HospedajeExists(int id, string codMascota)
        {
            return _context.Hospedajes.Any(e => e.IdHospedaje == id && e.CodMascota == codMascota);
        }
    }
}
