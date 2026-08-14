using ControlEquipos.Data;
using ControlEquipos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEquipos.Controllers
{
    public class EquiposController : Controller
    {
        private readonly ControlEquiposContext _context;

        // MVC inyecta el contexto registrado en Program.cs para centralizar aquí
        // el acceso de las acciones CRUD a la base de datos.
        public EquiposController(ControlEquiposContext context)
        {
            _context = context;
        }

        // INDEX: consulta todos los equipos. ToListAsync ejecuta la consulta sin bloquear
        // el hilo y la lista resultante se envía como modelo a la vista.
        public async Task<IActionResult> Index()
        {
            var equipos = await _context.Equipos.ToListAsync();

            return View(equipos);
        }

        // DETAILS: valida el Id y busca el registro que se mostrará en modo de solo lectura.
        // NotFound produce una respuesta 404 si no se recibió el Id o el equipo no existe.
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

        // EDIT GET: localiza el equipo solicitado y carga sus datos iniciales en el formulario.
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

        // DELETE GET: recupera el equipo para mostrar sus datos antes de pedir confirmación.
        // Esta acción no elimina información; la modificación real se reserva para el POST.
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

        // DELETE POST: HttpPost evita que una simple navegación elimine datos y el token
        // antifalsificación comprueba que la solicitud provenga del formulario de la aplicación.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // FindAsync busca el registro por su clave primaria antes de marcarlo para eliminación.
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo != null)
            {
                // Remove cambia el estado de la entidad y SaveChangesAsync ejecuta el DELETE en SQL Server.
                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();

                // TempData conserva el mensaje durante la redirección hacia el listado.
                TempData["MensajeExito"] =
                    "Equipo eliminado correctamente.";
            }

            // RedirectToAction vuelve a consultar Index y evita repetir el POST al actualizar la página.
            return RedirectToAction(nameof(Index));
        }

        // EDIT POST: recibe mediante model binding el Id de la ruta y los valores enviados
        // por el formulario; el token antifalsificación protege la operación de actualización.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Equipo equipo)
        {
            // Impide actualizar un registro diferente si el Id de la ruta y el del formulario no coinciden.
            if (id != equipo.Id)
            {
                return NotFound();
            }

            // Comprueba si otro equipo ya usa el Serial, excluyendo de la búsqueda el registro actual.
            if (!string.IsNullOrWhiteSpace(equipo.Serial))
            {
                var serialExiste = await _context.Equipos
                    .AnyAsync(e => e.Serial == equipo.Serial && e.Id != id);

                if (serialExiste)
                {
                    // Agrega el problema al ModelState para presentarlo junto al campo Serial.
                    ModelState.AddModelError(
                        nameof(equipo.Serial),
                        "Ya existe un equipo con este serial");
                }
            }

            // Solo persiste la edición cuando se cumplen las DataAnnotations y las reglas adicionales.
            if (ModelState.IsValid)
            {
                // FindAsync obtiene una entidad seguida por Entity Framework Core;
                // los cambios sobre ella serán detectados al guardar.
                var equipoExistente = await _context.Equipos.FindAsync(id);

                if (equipoExistente == null)
                {
                    return NotFound();
                }

                // La copia explícita limita la actualización a las propiedades permitidas del formulario.
                equipoExistente.Nombre = equipo.Nombre;
                equipoExistente.Tipo = equipo.Tipo;
                equipoExistente.Marca = equipo.Marca;
                equipoExistente.Modelo = equipo.Modelo;
                equipoExistente.Serial = equipo.Serial;
                equipoExistente.Estado = equipo.Estado;

                // SaveChangesAsync genera y ejecuta el UPDATE correspondiente en la base de datos.
                await _context.SaveChangesAsync();

                // TempData permite mostrar la confirmación después de redirigir al listado.
                TempData["MensajeExito"] =
                    "Equipo actualizado correctamente.";

                // RedirectToAction completa el patrón POST-Redirect-GET y vuelve al listado actualizado.
                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }

        // CREATE GET: muestra un formulario vacío para registrar un nuevo equipo.
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST: el model binding construye Equipo con los datos del formulario;
        // HttpPost y el token antifalsificación protegen el envío que modifica información.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipo equipo)
        {
            // AnyAsync comprueba en la base de datos si otro equipo ya utiliza el mismo Serial.
            if (!string.IsNullOrWhiteSpace(equipo.Serial))
            {
                var serialExiste = await _context.Equipos
                    .AnyAsync(e => e.Serial == equipo.Serial);

                if (serialExiste)
                {
                    // Incorpora el error al campo Serial antes de evaluar toda la validación del modelo.
                    ModelState.AddModelError(
                        nameof(equipo.Serial),
                        "Ya existe un equipo con este serial");
                }
            }

            // Solo agrega la entidad cuando las DataAnnotations y la unicidad del Serial son válidas.
            if (ModelState.IsValid)
            {
                // Add comienza el seguimiento de la entidad y SaveChangesAsync ejecuta el INSERT.
                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                // El mensaje sobrevive a la redirección y se muestra una vez en la vista Index.
                TempData["MensajeExito"] =
                    "Equipo registrado correctamente.";

                // RedirectToAction evita reenviar el formulario si el usuario actualiza la página siguiente.
                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }
    }
}
