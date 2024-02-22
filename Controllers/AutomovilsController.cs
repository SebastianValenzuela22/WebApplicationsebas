using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationsebas.Context;
using WebApplicationsebas.Models;

namespace WebApplicationsebas.Controllers
{
    public class AutomovilsController : Controller
    {
        private readonly SebasContext _context;

        public AutomovilsController(SebasContext context)
        {
            _context = context;
        }

        // GET: Automovils
        public async Task<IActionResult> Index()
        {
            return View(await _context.Automovil.ToListAsync());
        }

        // GET: Automovils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }

        // GET: Automovils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Automovils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modelo,Año,Foto")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(automovil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(automovil);
        }

        // GET: Automovils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil.FindAsync(id);
            if (automovil == null)
            {
                return NotFound();
            }
            return View(automovil);
        }

        // POST: Automovils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Año,Foto")] Automovil automovil)
        {
            if (id != automovil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(automovil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomovilExists(automovil.Id))
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
            return View(automovil);
        }

        // GET: Automovils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }

        // POST: Automovils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var automovil = await _context.Automovil.FindAsync(id);
            if (automovil != null)
            {
                _context.Automovil.Remove(automovil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomovilExists(int id)
        {
            return _context.Automovil.Any(e => e.Id == id);
        }
    }
}
