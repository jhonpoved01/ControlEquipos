using System.ComponentModel.DataAnnotations;

namespace ControlEquipos.Models
{
    // Representa un equipo dentro del flujo MVC y se utiliza tanto en las vistas
    // como en las operaciones de Entity Framework Core sobre la base de datos.
    public class Equipo
    {
        public int Id { get; set; }

        // Estas DataAnnotations declaran reglas compartidas por el model binding y la validación:
        // Required impide valores vacíos y StringLength limita el tamaño permitido de los textos.
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo no puede superar los 50 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100, ErrorMessage = "El modelo no puede superar los 100 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El serial es obligatorio")]
        [StringLength(100, ErrorMessage = "El serial no puede superar los 100 caracteres")]
        public string Serial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres")]
        // La expresión regular restringe el estado a las tres opciones admitidas por la aplicación.
        [RegularExpression("^(Disponible|En uso|Mantenimiento)$", ErrorMessage = "El estado seleccionado no es válido")]
        public string Estado { get; set; } = string.Empty;
    }
}
