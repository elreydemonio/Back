using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NgZorroBack.Models;
using NgZorroBack.Models.Join;
using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NgZorroBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        public const string SessionKeyName = "_Name";
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly ConfiguracionGlobal _configuracionGlobal;
        private readonly MerakiZorroContext _context;
        private readonly string ConectionString;
        private readonly ValidarUsuario validation = new ValidarUsuario();
        public ServiciosController(UserManager<UsuarioIdentity> userManager,
            SignInManager<UsuarioIdentity> signInManager, IOptions<ConfiguracionGlobal> configuracionGlobal, MerakiZorroContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuracionGlobal = configuracionGlobal.Value;
            _context = context;
            ConectionString = "Server=DESKTOP-EMH23CH;Database=NgZorroMerakiF4;Trusted_Connection=True;";
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConectionString);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            string usuarioId = User.Claims.First(c => c.Type == "UsuarioID").Value;
            string usuarioIRol = User.Claims.First(c => c.Type == "Rol").Value;
            int idRol = Convert.ToInt32(usuarioIRol);
            using (IDbConnection dbConnection = Connection)
            {
                if (idRol == 1)
                {
                    string sQuery = @"select s.IdServicio ,s.CelularRecibe,s.DireccionCarga,s.DireccionEntrega,s.FechaFin,s.IdConductor,s.IdEstadoServicio,s.PersonaRecibe,s.PrecioServicio,
                                                T.DescripcionCarga, E.Estado, E.IdEstadoServicio
                                                from Servicios s 
			                                    join UsuariosIdentity C on s.IdCliente = C.Id
			                                    join TipoCargas T On s.IdTipoCarga = T.IdTipoCarga
			                                    join EstadoServicios E On s.IdEstadoServicio = E.IdEstadoServicio
			                                    join InfoConductores I On s.IdConductor = I.IdInfo ";
                    dbConnection.Open();
                    return Ok(await dbConnection.QueryAsync<object>(sQuery));
                }
                if (idRol == 3)
                {
                    string sQuery = @"select s.IdServicio ,s.CelularRecibe,s.DireccionCarga,s.DireccionEntrega,s.FechaFin,s.IdConductor,s.IdEstadoServicio,s.PersonaRecibe,s.PrecioServicio,
                                    T.DescripcionCarga, E.Estado, E.IdEstadoServicio
                                    from Servicios s 
			                        join UsuariosIdentity C on s.IdCliente = C.Id
			                        join TipoCargas T On s.IdTipoCarga = T.IdTipoCarga
			                        join EstadoServicios E On s.IdEstadoServicio = E.IdEstadoServicio
			                        join InfoConductores I On s.IdConductor = I.IdInfo 
                                    where s.IdCliente = @idCliente";

                    dbConnection.Open();
                    return Ok(await dbConnection.QueryAsync<object>(sQuery, new { idCliente = usuarioId }));
                }
                else
                {
                    return NoContent();
                }
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(int id)
        {
            string usuarioIRol = User.Claims.First(c => c.Type == "Rol").Value;
            int idRol = Convert.ToInt32(usuarioIRol);
            Servicio servicio = await _context.Servicios.FindAsync(id);
            if (servicio is null)
            {
                return NoContent();
            }
            using (IDbConnection dbConnection = Connection)
            {
                if (idRol == 1)
                {
                    string sQuery = @"select  s.IdServicio , s.CelularRecibe,s.DireccionCarga,s.DireccionEntrega,s.FechaFin,s.IdConductor,s.IdEstadoServicio,s.PersonaRecibe,s.PrecioServicio,
                                       T.DescripcionCarga, c.Nombre + ' ' + c.Apellido As 'NombreCliente',  V.Nombre + ' ' + V.Apellido As 'NombreConductor', s.FechaInicio, E.Estado, E.IdEstadoServicio, s.CelularRecibe
                                       from Servicios s 
			                            join UsuariosIdentity C on s.IdCliente = C.Id
			                            join TipoCargas T On s.IdTipoCarga = T.IdTipoCarga
			                            join EstadoServicios E On s.IdEstadoServicio = E.IdEstadoServicio
			                            join InfoConductores I On s.IdConductor = I.IdInfo 
			                            join UsuariosIdentity V On i.IdConductor = V.Id
                                        where s.IdServicio = @Id";
                    dbConnection.Open();
                    return Ok(await dbConnection.QueryAsync<object>(sQuery, new { Id = id }));
                }
                if (idRol == 3)
                {
                    string sQuery = @"select  s.IdServicio , s.CelularRecibe,s.DireccionCarga,s.DireccionEntrega,s.FechaFin,s.IdConductor,s.IdEstadoServicio,s.PersonaRecibe,s.PrecioServicio,
                                        T.DescripcionCarga, s.FechaInicio, E.Estado, E.IdEstadoServicio, s.CelularRecibe
                                        from Servicios s 
			                            join UsuariosIdentity C on s.IdCliente = C.Id
			                            join TipoCargas T On s.IdTipoCarga = T.IdTipoCarga
			                            join EstadoServicios E On s.IdEstadoServicio = E.IdEstadoServicio
			                            join InfoConductores I On s.IdConductor = I.IdInfo	
                                        where s.IdServicio = @Id";
                    dbConnection.Open();
                    return Ok(await dbConnection.QueryAsync<object>(sQuery, new { Id = id }));
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(ServiciosModel servicios)
        {
            string dateString =servicios.FechaFin;
            string format = "dd/MM/yyyy";
            DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            Servicio servicio = new Servicio()
            {
                CelularRecibe = servicios.CelularRecibe,
                PersonaRecibe = servicios.PersonaRecibe,
                DireccionCarga = servicios.DireccionCarga,
                DireccionEntrega = servicios.DireccionEntrega,
                FechaFin = dateTime,
                FechaInicio = DateTime.Now,
                IdCliente = User.Claims.First(c => c.Type == "UsuarioID").Value,
                IdConductor = null,
                IdEstadoServicio = 1,
                IdTipoCarga = servicios.IdTipoCarga,
                PrecioServicio = servicios.PrecioServicio
            };
            _context.Servicios.Add(servicio);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(servicio.IdServicio);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, int idConductor)
        {
            Servicio servicio = await _context.Servicios.FindAsync(id);
            InfoConductore infoConductore = await _context.InfoConductores.FindAsync(idConductor);
            if (servicio is null)
            {
                return NoContent();
            }
            else
            {
                if(infoConductore is null)
                {
                    return BadRequest();
                }
                else
                {
                    Vehiculo vehiculo = await _context.Vehiculos.FindAsync(infoConductore.CodigoV);
                    if (vehiculo is null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        servicio.IdConductor = idConductor;
                        _context.Servicios.Update(servicio);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }
        }
        [HttpGet]
        [Route("Vehiculos/{id}")]
        public async Task<ActionResult> Vehiculos(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select  s.IdServicio , s.IdConductor, V.CodigoV, V.FotoV, C.Nombre+' '+C.Apellido As 'NombreConductor', V.Placa, Tv.Descripcion
                                    from Servicios s 
									join InfoConductores I On s.IdConductor = I.IdInfo
			                        join UsuariosIdentity C on I.IdConductor = C.Id
			                        join TipoCargas T On s.IdTipoCarga = T.IdTipoCarga
			                        join EstadoServicios E On s.IdEstadoServicio = E.IdEstadoServicio
			                        join Vehiculos V On I.CodigoV = V.CodigoV
			                        join EstadoVehiculos O On V.IdEstadoVehiculo = O.IdEstadoVehiculo
									join TipoVehiculos Tv On v.IdTipoVehiculo = Tv.IdTipoVehiculo 
			                        where O.IdEstadoVehiculo = 1";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<object>(sQuery, new { Id = id }));
            }
        }
        [HttpGet]
        [Route("Conductor/{id}")]
        public async Task<ActionResult> Conductor(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select I.FotoConductor,I.FechaInicio, U.Nombre+' '+U.Apellido As 'NombreConductor',U.Celular, I.IdConductor from InfoConductores I 
									join UsuariosIdentity U On I.IdConductor = U.Id
									where I.IdInfo = @id";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<object>(sQuery, new { Id = id }));
            }
        }
        [HttpGet]
        [Route("ListarTiposDeCarga")]
        public async Task<ActionResult> TipoCargar()
        {
            return Ok(await _context.TipoCargas.ToListAsync());
        }
        [HttpGet]
        [Route("AceptarServicio/{id}")]
        public async Task<ActionResult> AceptarServicio(int id)
        {
            var servcio = await _context.Servicios.FindAsync(id);
            if (servcio is null)
            {
                return BadRequest();
            }
            else
            {
                using (IDbConnection dbConnection = Connection)
                { 
                    string QueryUpdateSinAceptar = @"Update Servicios set IdEstadoServicio = 3 where IdServicio != @Id";
                    string QueryUpdateAceptar = @"Update Servicios set IdEstadoServicio = 2 where IdServicio = @Id";
                    string QueryVehiculo = @"select * from Vehiculos v where v.CodigoV = @Id";
                    dbConnection.Open();
                    var OkSinAceptar = await dbConnection.ExecuteAsync(QueryUpdateSinAceptar, new { Id = id });
                    var OkAceptar = await dbConnection.ExecuteAsync(QueryUpdateAceptar, new { Id = id });
                    var OkVehiculo = await dbConnection.QueryFirstAsync<Vehiculo>(QueryVehiculo, new { Id = id });
                    OkVehiculo.IdEstadoVehiculo = 3;
                    _context.Vehiculos.Update(OkVehiculo);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
        }
        [HttpGet]
        [Route("CancelarServicio/{id}")]
        public async Task<ActionResult> CancelarServicio(int id)
        {
            var servcio = await _context.Servicios.FindAsync(id);
            if (servcio is null)
            {
                return BadRequest();
            }
            else
            {
                if(servcio.FechaFin >= DateTime.Now)
                {
                    return BadRequest();
                }
                else
                {
                using (IDbConnection dbConnection = Connection)
                {
                        string QueryVehiculo = @"select * from Vehiculos v where v.CodigoV = @Id";
                        var OkVehiculo = await dbConnection.QueryFirstAsync<Vehiculo>(QueryVehiculo, new { Id = id });
                        OkVehiculo.IdEstadoVehiculo = 1;
                        _context.Vehiculos.Update(OkVehiculo);
                        await _context.SaveChangesAsync();
                    }
                    int estado = 3;
                    servcio.IdEstadoServicio = estado;
                    _context.Servicios.Update(servcio);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }  
        }

        [HttpGet]
        [Authorize]
        [Route("ServiciosPorAceptar")]
        public async Task<ActionResult> ServiciosConductor()
        {
            var id = User.Claims.First(c => c.Type == "UsuarioID").Value;
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select c.Nombre + ' ' +c.Apellido,s.DireccionCarga,s.DireccionEntrega, T.DescripcionCarga,s.PrecioServicio from Servicios S
									join UsuariosIdentity C On S.IdCliente = C.Id
									join TipoCargas T On S.IdTipoCarga = T.IdTipoCarga
									join InfoConductores I On S.IdConductor = I.IdInfo
									join UsuariosIdentity Co On I.IdConductor = Co.Id
									where Co.Id = @Id";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<object>(sQuery, new { Id = id }));
            }
        }
    }
}
