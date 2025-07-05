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
    public class AlimentosController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public AlimentosController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Alimentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alimentos.ToListAsync());
        }

        // GET: Alimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alimento = await _context.Alimentos
                .FirstOrDefaultAsync(m => m.CodAlimento == id);
            if (alimento == null)
            {
                return NotFound();
            }

            return View(alimento);
        }

        // GET: Alimentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodAlimento,Nombre,Descripcion,Proveedor,PrecioUnitario")] Alimento alimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alimento);
        }

        // GET: Alimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento == null)
            {
                return NotFound();
            }
            return View(alimento);
        }

        // POST: Alimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodAlimento,Nombre,Descripcion,Proveedor,PrecioUnitario")] Alimento alimento)
        {
            if (id != alimento.CodAlimento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentoExists(alimento.CodAlimento))
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
            return View(alimento);
        }

        // GET: Alimentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alimento = await _context.Alimentos
                .FirstOrDefaultAsync(m => m.CodAlimento == id);
            if (alimento == null)
            {
                return NotFound();
            }

            return View(alimento);
        }

        // POST: Alimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento != null)
            {
                _context.Alimentos.Remove(alimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentoExists(int id)
        {
            return _context.Alimentos.Any(e => e.CodAlimento == id);
        }
    }
}
