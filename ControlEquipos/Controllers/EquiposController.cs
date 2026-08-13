using ControlEquipos.Data;
using ControlEquipos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEquipos.Controllers
{
    public class EquiposController : Controller
    {
        private readonly ControlEquiposContext _context;

        public EquiposController(ControlEquiposContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var equipos = await _context.Equipos.ToListAsync();

            return View(equipos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(equipo => equipo.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(equipo => equipo.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(equipo => equipo.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Equipo equipo)
        {
            if (id != equipo.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(equipo.Serial))
            {
                var serialExiste = await _context.Equipos
                    .AnyAsync(e => e.Serial == equipo.Serial && e.Id != id);

                if (serialExiste)
                {
                    ModelState.AddModelError(
                        nameof(equipo.Serial),
                        "Ya existe un equipo con este serial");
                }
            }

            if (ModelState.IsValid)
            {
                var equipoExistente = await _context.Equipos.FindAsync(id);

                if (equipoExistente == null)
                {
                    return NotFound();
                }

                equipoExistente.Nombre = equipo.Nombre;
                equipoExistente.Tipo = equipo.Tipo;
                equipoExistente.Marca = equipo.Marca;
                equipoExistente.Modelo = equipo.Modelo;
                equipoExistente.Serial = equipo.Serial;
                equipoExistente.Estado = equipo.Estado;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipo equipo)
        {
            if (!string.IsNullOrWhiteSpace(equipo.Serial))
            {
                var serialExiste = await _context.Equipos
                    .AnyAsync(e => e.Serial == equipo.Serial);

                if (serialExiste)
                {
                    ModelState.AddModelError(
                        nameof(equipo.Serial),
                        "Ya existe un equipo con este serial");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }
    }
}
