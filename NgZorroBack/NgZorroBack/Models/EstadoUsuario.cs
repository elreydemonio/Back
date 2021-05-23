using NgZorroBack.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class EstadoUsuario
    {
        public EstadoUsuario()
        {
            Usuarios = new HashSet<UsuarioIdentity>();
        }
        [Key]
        public int IdEstadoUsuario { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<UsuarioIdentity> Usuarios { get; set; }
    }
}
