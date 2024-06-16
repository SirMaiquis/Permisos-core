using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Permiso : BaseEntity
    {
        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public string ApellidosEmpleado { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

        [Required]
        public int TipoPermisoId { get; set; }

        public TipoPermiso TipoPermiso { get; set; }
    }
}
