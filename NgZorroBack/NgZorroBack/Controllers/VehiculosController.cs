using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NgZorroBack.Models;
using NgZorroBack.Models.Join;
using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NgZorroBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {

        private string fotoV;
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly MerakiZorroContext _context;
        private readonly string ConectionString;
        public VehiculosController(MerakiZorroContext context, UserManager<UsuarioIdentity> userManager)
        {
            _userManager = userManager;
            _context = context;
            ConectionString = "Server=DESKTOP-DER5DC8\\SQLEXPRESS;Database=NgZorroMerakiF3;Trusted_Connection=True;";
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConectionString);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculosJoin>>> GetVehiculos()
        {
            using (IDbConnection dbConnection = Connection)
            {

                string sQuery = @"select G.CodigoV, M.NombreMarca, G.Modelo, C.NombreColor, G.Cilindraje, G.Soat, G.TecnoMecanica, G.Placa , G.FotoV, G.SeguroCarga, U.Nombre, U.Id, T.Descripcion, E.NombreEstadoV
                                from Vehiculos G
                                join Colores C On G.IdColor = C.Id
                                join Marcas M On G.IdMarca = M.Id
                                join UsuariosIdentity U On G.IdPropietario = U.Id
                                join TipoVehiculos T On G.IdTipoVehiculo = T.IdTipoVehiculo
								join EstadoVehiculos E ON G.IdEstadoVehiculo = E.IdEstadoVehiculo";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<VehiculosJoin>(sQuery));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post(Vehiculo vehiculo)
        {
            int longitud = 7;
            Guid miGuid = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "");
            string codigoV = token.Substring(0, longitud);
            vehiculo.CodigoV = codigoV;
            vehiculo.IdPropietario = "aeaf5a0b-6d47-4004-90ac-b9463346b56d";
            string Fotov = Path.GetFileName(vehiculo.FotoV);
            string Seguro = Path.GetFileName(vehiculo.SeguroCarga);
            string soat = Path.GetFileName(vehiculo.Soat);
            string tecnomecanica = Path.GetFileName(vehiculo.TecnoMecanica);
            vehiculo.FotoV = Fotov;
            vehiculo.SeguroCarga = Seguro;
            vehiculo.Soat = soat;
            vehiculo.TecnoMecanica = tecnomecanica;
            vehiculo.IdEstadoVehiculo = 1;
            _context.Vehiculos.Add(vehiculo);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                if (VehiculoExists(vehiculo.CodigoV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool VehiculoExists(string id)
        {
            return _context.Vehiculos.Any(e => e.CodigoV == id);
        }
        [HttpPost]
        [Route("Imagenes")]
        public async Task<IActionResult> imagenes(IFormFile File)
        {
            var files = Request.Form.Files[0];
            string move = $"D:\\Angular\\ng-zorro\\src\\assets\\img";
            using (var fileStream = new FileStream(Path.Combine(move, File.FileName), FileMode.Create, FileAccess.Write))
            {
                await File.CopyToAsync(fileStream);
            }
            return Ok();
        }
        [HttpGet]
        [Route("DetalleVehiculo/{id}")]
        public async Task<ActionResult<VehiculosJoin>> Vehiculos(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {

                string sQuery = @"select G.CodigoV, M.NombreMarca, G.Modelo, C.NombreColor, G.Cilindraje, G.Soat, G.TecnoMecanica, G.Placa , G.FotoV, G.SeguroCarga, U.Nombre, U.Id, T.Descripcion, E.NombreEstadoV
                                from Vehiculos G
                                join Colores C On G.IdColor = C.Id
                                join Marcas M On G.IdMarca = M.Id
                                join UsuariosIdentity U On G.IdPropietario = U.Id
                                join TipoVehiculos T On G.IdTipoVehiculo = T.IdTipoVehiculo
								join EstadoVehiculos E ON G.IdEstadoVehiculo = E.IdEstadoVehiculo
								where G.CodigoV = @id
                                ";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<VehiculosJoin>(sQuery, new { Id = id }));
            }
        } 
        [HttpGet]
        [Route("Tipovehiculos")]
        public async Task<ActionResult<IEnumerable<TipoVehiculo>>> GetTipoVechiuslo()
        {
            return await _context.TipoVehiculos.ToListAsync();
        }
        [HttpGet]
        [Route("Marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarca()
        {
            return await _context.Marcas.ToListAsync();
        }
        [HttpGet]
        [Route("Colores")]
        public async Task<ActionResult<IEnumerable<Colore>>> GetColores()
        {
            return await _context.Colores.ToListAsync();
        }
        [HttpPut]
        [Route("CambiarEstado/{id}")]
        public async Task<ActionResult> CambiarEstadoVehiculo(string id, string codigoV)
        {
            var Vehiculo = await _context.Vehiculos.FindAsync(id);
            if(Vehiculo is null)
            {
                return NoContent();
            }
            if (Vehiculo.IdEstadoVehiculo == 1)
            {
                Vehiculo.IdEstadoVehiculo = 2;
                _context.Vehiculos.Update(Vehiculo);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else if(Vehiculo.IdEstadoVehiculo == 2)
            {
                Vehiculo.IdEstadoVehiculo = 1;
                _context.Vehiculos.Update(Vehiculo);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Ok();
        }
    }
}
