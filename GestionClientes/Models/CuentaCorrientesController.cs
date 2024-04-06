using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionClientes.Models
{
    public class CuentaCorrientesController : Controller
    {
        private readonly controlGlobalContext _context;

        public CuentaCorrientesController(controlGlobalContext context)
        {
            _context = context;
        }

        // GET: CuentaCorrientes
        public async Task<IActionResult> Index()
        {
            var controlGlobalContext = _context.CuentaCorrientes.Include(c => c.Cliente);
            return View(await controlGlobalContext.ToListAsync());
        }

        // GET: CuentaCorrientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CuentaCorrientes == null)
            {
                return NotFound();
            }

            var cuentaCorriente = await _context.CuentaCorrientes
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (cuentaCorriente == null)
            {
                return NotFound();
            }

            return View(cuentaCorriente);
        }

        // GET: CuentaCorrientes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            return View();
        }

        // POST: CuentaCorrientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimientoId,ClienteId,FhMovimiento,Importe,Descripcion")] CuentaCorriente cuentaCorriente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuentaCorriente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", cuentaCorriente.ClienteId);
            return View(cuentaCorriente);
        }

        // GET: CuentaCorrientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CuentaCorrientes == null)
            {
                return NotFound();
            }

            var cuentaCorriente = await _context.CuentaCorrientes.FindAsync(id);
            if (cuentaCorriente == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", cuentaCorriente.ClienteId);
            return View(cuentaCorriente);
        }

        // POST: CuentaCorrientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimientoId,ClienteId,FhMovimiento,Importe,Descripcion")] CuentaCorriente cuentaCorriente)
        {
            if (id != cuentaCorriente.MovimientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentaCorriente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaCorrienteExists(cuentaCorriente.MovimientoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", cuentaCorriente.ClienteId);
            return View(cuentaCorriente);
        }

        // GET: CuentaCorrientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CuentaCorrientes == null)
            {
                return NotFound();
            }

            var cuentaCorriente = await _context.CuentaCorrientes
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (cuentaCorriente == null)
            {
                return NotFound();
            }

            return View(cuentaCorriente);
        }

        // POST: CuentaCorrientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CuentaCorrientes == null)
            {
                return Problem("Entity set 'controlGlobalContext.CuentaCorrientes'  is null.");
            }
            var cuentaCorriente = await _context.CuentaCorrientes.FindAsync(id);
            if (cuentaCorriente != null)
            {
                _context.CuentaCorrientes.Remove(cuentaCorriente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaCorrienteExists(int id)
        {
          return (_context.CuentaCorrientes?.Any(e => e.MovimientoId == id)).GetValueOrDefault();
        }
    }
}
