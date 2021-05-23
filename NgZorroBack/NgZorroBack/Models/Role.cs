using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<UsuarioIdentity>();
        }
        [Key]
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<UsuarioIdentity> Usuarios { get; set; }
    }
}

