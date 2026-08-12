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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipo equipo)
        {
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
