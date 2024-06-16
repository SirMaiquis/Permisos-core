using System.ComponentModel.DataAnnotations;

namespace Services.Dtos
{
    public class PermisoDTO : BaseDto.BaseDto
    {
        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public string ApellidosEmpleado { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

        [Required]
        public int TipoPermisoId { get; set; }

        public string? TipoPermisoDescripcion { get; set; }
    }
}