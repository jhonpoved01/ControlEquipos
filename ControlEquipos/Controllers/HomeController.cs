using ControlEquipos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControlEquipos.Controllers
{
    public class HomeController : Controller
    {
        // Atiende la ruta inicial y permite que MVC presente la vista principal del sistema.
        public IActionResult Index()
        {
            return View();
        }

        // Evita almacenar en caché la respuesta de error y entrega a la vista un identificador
        // que facilita relacionar el mensaje mostrado con la solicitud que falló.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
