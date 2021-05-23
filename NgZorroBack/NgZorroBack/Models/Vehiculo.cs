using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            InfoConductores = new HashSet<InfoConductore>();
        }
        [Key]
        public int CodigoV { get; set; }
        public int IdMarca { get; set; }
        public int Modelo { get; set; }
        public int IdColor { get; set; }
        public int Cilindraje { get; set; }
        public string Soat { get; set; }
        public string TecnoMecanica { get; set; }
        public string Placa { get; set; }
        public string FotoV { get; set; }
        public string SeguroCarga { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdTipoVehiculo { get; set; }
        public int? IdEstadoVehiculo { get; set; }

        public virtual Colore IdColorNavigation { get; set; }
        public virtual EstadoVehiculo IdEstadoVehiculoNavigation { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
        public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; }
        public virtual ICollection<InfoConductore> InfoConductores { get; set; }
    }
}
