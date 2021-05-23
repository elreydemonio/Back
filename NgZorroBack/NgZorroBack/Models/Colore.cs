using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public partial class Colore
    {
        public Colore()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}