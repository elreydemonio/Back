using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Usuarios = new HashSet<UsuarioIdentity>();
        }
        [Key]
        public int IdGenero { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<UsuarioIdentity> Usuarios { get; set; }
    }
}
