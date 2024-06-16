using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class TipoPermiso : BaseEntity
    {
        [Required]
        public string Descripcion { get; set; }
        public ICollection<Permiso> Permisos { get; set; }
    }
}
