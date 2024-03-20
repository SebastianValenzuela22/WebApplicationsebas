using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationsebas.Context;
using WebApplicationsebas.Models;
using Microsoft.Extensions.Options;
using WebApplicationsebas.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace WebApplicationsebas.Controllers
{
    public class AutomovilsController : Controller
    {
        private readonly SebasContext _context;
        private readonly AzureStorageConfig _config;

        public AutomovilsController(SebasContext context, IOptions<AzureStorageConfig>config)
        {
            _context = context;
            _config = config.Value;
        }

        // GET: Automovils
        public async Task<IActionResult> Index()
        {
            //lineas cambiadas
            return View(await _context.Automoviles.Include(e=> e.conductor).ToListAsync());
        }

        // GET: Automovils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Automoviles == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automoviles.Include(e=>e.conductor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }

        // GET: Automovils/Create
        public async Task<IActionResult> Create()
        {
            var conductores = await _context.Conductores.ToListAsync();
            ViewBag.conductor = new SelectList(conductores, "Id", "NombreConductor");
            return View();
        }

        // POST: Automovils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modelo,Año,Foto,conductor")] Automovil automovil, IFormFile foto)
        {
            if ("Modelo,Año,Foto,Conductor.Id".Split(',').All(campo=>ModelState.ContainsKey(campo)))
            {
                if(foto == null)
                {
                    automovil.Foto = StorageHelper.URL_Imagen_default;
                }
                else
                {
                    string extension = foto.FileName.Split(",")[0];
                    string nombre = $"{Guid.NewGuid()}.{extension}";
                    automovil.Foto = await StorageHelper.SubirArchivo(foto.OpenReadStream(), nombre, _config);

                }
                _context.Set<Automovil>().Add(automovil);
                _context.Entry(automovil.conductor).State = EntityState.Unchanged;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(automovil);
        }

        // GET: Automovils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Automoviles == null)
            {
                return NotFound();
            }

            var conductores = await _context.Conductores.ToListAsync();
            ViewBag.conductor = new SelectList(conductores, "Id", "NombreConductor");
            var Automovil = await _context.Automoviles.FindAsync(id);
            if (Automovil == null)
            {
                return NotFound();
            }
            return View(Automovil);
        }

        // POST: Automovils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Año,Foto,conductor")] Automovil automovil , IFormFile foto)
        {
            if (id != automovil.Id)
            {
                return NotFound();
            }

            if ("Modelo,Año,Foto,Conductor.Id".Split(',').All(campo => ModelState.ContainsKey(campo)))
            {
                var datos = await _context.Conductores.FindAsync(automovil.conductor.Id);
                automovil.conductor = datos;
                try
                {
                    if (foto == null)
                    {
                        //automovil.Foto = automovil.Foto;
                       //automovil.Foto = StorageHelper.URL_Imagen_default;
                      // string extension = foto.FileName.Split(",")[1];
                      // string nombre = $"{Guid.NewGuid()}.{extension}";
                      // automovil.Foto = await StorageHelper.SubirArchivo(foto.OpenReadStream(), nombre, _config);
                        
                    }
                    else
                    {
                        string extension = foto.FileName.Split(",")[0];
                        string nombre = $"{Guid.NewGuid()}.{extension}";
                        automovil.Foto = await StorageHelper.SubirArchivo(foto.OpenReadStream(), nombre, _config);
                    }
                    
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
            if (id == null || _context.Automoviles == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automoviles.Include(e=>e.conductor)
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
            if (_context.Automoviles == null)
            {
                return Problem("Entity set 'jaragongcontext.Automoviles' is null.");
            }

            var automovil = await _context.Automoviles.FindAsync(id);
            if (automovil != null)
            {
                _context.Automoviles.Remove(automovil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomovilExists(int id)
        {
            return (_context.Automoviles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
