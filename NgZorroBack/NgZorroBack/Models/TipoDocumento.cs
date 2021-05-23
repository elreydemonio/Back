using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Usuarios = new HashSet<UsuarioIdentity>();
        }
        [Key]
        public int IdTipoDocumento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<UsuarioIdentity> Usuarios { get; set; }
    }
}