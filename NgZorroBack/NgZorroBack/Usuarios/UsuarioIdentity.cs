using Microsoft.AspNetCore.Identity;
using NgZorroBack.Models;

namespace NgZorroBack.Usuarios
{
    public class UsuarioIdentity : IdentityUser
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdGenero { get; set; }
        public string Direccion { get; set; }
        public int NumeroDocumento { get; set; }
        public int Celular { get; set; }

        public virtual EstadoUsuario IdEstadoNavigation { get; set; }
        public virtual Genero IdGeneroNavigation { get; set; }
        public virtual Role IdRolNavigation { get; set; }
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
    }
}
