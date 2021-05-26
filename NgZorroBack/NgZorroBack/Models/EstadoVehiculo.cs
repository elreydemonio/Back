using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgZorroBack.Models
{
    public partial class EstadoVehiculo
    {
        public EstadoVehiculo()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
        [Key]
        public int IdEstadoVehiculo { get; set; }
        public string NombreEstadoV { get; set; }

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
