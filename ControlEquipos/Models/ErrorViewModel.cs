namespace ControlEquipos.Models
{
    // Transporta a la vista de error el identificador de la solicitud, cuando está disponible.
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
