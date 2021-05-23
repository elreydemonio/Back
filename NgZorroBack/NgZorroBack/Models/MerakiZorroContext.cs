using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Models
{
    public class MerakiZorroContext : DbContext
    {
        public MerakiZorroContext(DbContextOptions<MerakiZorroContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioIdentity> UsuariosIdentity { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EstadoUsuario> EstadoUsuarios { get; set; }
        public DbSet<InfoConductore> InfoConductores { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Colore> Colores { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<EstadoVehiculo> EstadoVehiculos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<EstadoServicio> EstadoServicios { get; set; }
        public DbSet<TipoCarga> TipoCargas { get; set; }


    }
}
