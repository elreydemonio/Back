using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Usuarios
{
    public class UsuarioModel
    {
        public string NombreUsuario { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdGenero { get; set; }
        public string Direccion { get; set; }
        public int NumeroDocumento { get; set; }
        public int Celular { get; set; }
    }
}
