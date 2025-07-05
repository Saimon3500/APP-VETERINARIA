using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly VeterinariaExtendidaContext _context;

        public ClientesController(VeterinariaExtendidaContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.CodCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCliente,Apellido,Banco,Correo,CuentaBanco,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC InsertCliente @CodCliente = {0}, @Apellido = {1}, @Banco = {2}, @Correo = {3}, @CuentaBanco = {4}, @Direccion = {5}, @Telefono = {6}",
                        cliente.CodCliente, cliente.Apellido, cliente.Banco, cliente.Correo, cliente.CuentaBanco, cliente.Direccion, cliente.Telefono);

                    if (result == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Error inserting data, possible truncated values.");
                        return View(cliente);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodCliente,Apellido,Banco,Correo,CuentaBanco,Direccion,Telefono")] Cliente cliente)
        {
            if (id != cliente.CodCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC UpdateCliente @CodCliente = {0}, @Apellido = {1}, @Banco = {2}, @Correo = {3}, @CuentaBanco = {4}, @Direccion = {5}, @Telefono = {6}",
                        cliente.CodCliente, cliente.Apellido, cliente.Banco, cliente.Correo, cliente.CuentaBanco, cliente.Direccion, cliente.Telefono);

                    if (result == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Error updating data, possible truncated values.");
                        return View(cliente);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.CodCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteCliente @CodCliente = {0}", id);

                if (result == -1)
                {
                    ModelState.AddModelError(string.Empty, "Error deleting data.");
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }

        private bool ClienteExists(string id)
        {
            return _context.Clientes.Any(e => e.CodCliente == id);
        }
    }
}
