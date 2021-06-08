using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NgZorroBack.Models;
using NgZorroBack.Models.Join;
using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NgZorroBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public const string SessionKeyName = "_Name";
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly ConfiguracionGlobal _configuracionGlobal;
        private readonly MerakiZorroContext _context;
        private readonly string ConectionString;
        private readonly ValidarUsuario validation = new ValidarUsuario();
        public UsuariosController(UserManager<UsuarioIdentity> userManager,
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
        [HttpPost]
        [Route("Registro")]
        public async Task<Object> PostUsuario(UsuarioModel usuarioModel)
        {
            UsuarioIdentity usu = new UsuarioIdentity()
            {
                UserName = usuarioModel.NombreUsuario,
                Email = usuarioModel.Email,
                Nombre = usuarioModel.Nombre,
                IdEstado = 1,
                IdRol = usuarioModel.IdRol,
                Apellido = usuarioModel.Apellido,
                Celular =  usuarioModel.Celular,
                Direccion = usuarioModel.Direccion,
                IdGenero = usuarioModel.IdGenero,
                IdTipoDocumento = usuarioModel.IdTipoDocumento,
                NumeroDocumento = usuarioModel.NumeroDocumento
            };
            try
            {
                var result = await _userManager
                    .CreateAsync(usu, usuarioModel.Password).ConfigureAwait(false);
                return Ok(usu);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        //POST: /api/Usuario/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var usuario = await _userManager.FindByNameAsync(loginModel.NombreUsuario).ConfigureAwait(false);
            if (usuario != null && await _userManager.CheckPasswordAsync(usuario, loginModel.Password).ConfigureAwait(false))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UsuarioID", usuario.Id.ToString()),
                        new Claim("Rol", usuario.IdRol.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracionGlobal.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });

            }
            else
            {
                return BadRequest(new { mensaje = "Nombre de usuario o contraseña incorrecta" });
            }
        }
        [HttpGet]
        [Route("ListarTipoDocumento")]
        public async Task<IEnumerable<TipoDocumento>> GetTipoDocumentos()
        {
            return await _context.TipoDocumentos.ToListAsync();
        }
        [HttpGet]
        [Route("ListarGenero")]
        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }
        [HttpGet]
        [Route("ListarRoles")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.Where(x=> x.IdRol > 1 && x.IdRol < 4).ToListAsync();
        }
        [HttpGet]
        [Route("Perfil")]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<ActionResult> ObtenerPerfilUsuario()
        {
            string usuarioId = User.Claims.First(c => c.Type == "UsuarioID").Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId).ConfigureAwait(false);

            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest(new { mensaje = "No se encuentra el usuario" });
            }
        }
        [HttpPost]
        [Route("Imagenes")]
        public async Task<IActionResult> imagenes(IFormFile File)
        {
            var files = Request.Form.Files[0];
            string move = $"E:\\GitHub\\Sebastian\\MerakiFrontEnd\\src\\assets\\img";
            using (var fileStream = new FileStream(Path.Combine(move, File.FileName), FileMode.Create, FileAccess.Write))
            {
                await File.CopyToAsync(fileStream);
            }

            return NoContent();
        }
        [HttpGet]
        [Route("ListarCliPro")]
        public async Task<ActionResult> ListarClientePropitario()
        {
            using (IDbConnection dbConnection = Connection)
            {

                string sQuery = @"select u.Id, u.Apellido, U.Nombre, U.Celular, U.UserName, U.NumeroDocumento, U.Email, U.Direccion, 
                                    R.NombreRol, T.NombreDoocumento, G.NombreGenero, E.IdEstadoUsuario, E.EstadoNombre
                                    from UsuariosIdentity u
                                    inner join Roles R On U.IdRol = R.IdRol
                                    inner join TipoDocumentos T On U.IdTipoDocumento = T.IdTipoDocumento
                                    inner join Generos G On U.IdGenero = G.IdGenero
                                    inner join EstadoUsuarios E On U.IdEstado = E.IdEstadoUsuario
                                    where R.IdRol >= 1 and R.IdRol <=4";
                dbConnection.Open();
                var OkResult = await dbConnection.QueryAsync<object>(sQuery);
                return Ok(OkResult);
            }
        }
        [HttpGet]
        [Route("ListarConductor")]
        public async Task<ActionResult> ListarConductor()
        {
            using (IDbConnection dbConnection = Connection)
            {

                string sQuery = @"select u.Id, u.Apellido, U.Nombre, U.Celular, U.UserName, U.NumeroDocumento, U.Email, U.Direccion,
                                    R.NombreRol, T.NombreDoocumento, G.NombreGenero, E.IdEstadoUsuario, E.EstadoNombre,
									I.FotoConductor, I.FechaInicio, I.FechaFin, V.CodigoV
                                    from UsuariosIdentity u
                                    inner join Roles R On U.IdRol = R.IdRol
                                    inner join TipoDocumentos T On U.IdTipoDocumento = T.IdTipoDocumento
                                    inner join Generos G On U.IdGenero = G.IdGenero
                                    inner join EstadoUsuarios E On U.IdEstado = E.IdEstadoUsuario
									inner join InfoConductores I On U.Id = I.IdConductor
									inner join Vehiculos V On I.CodigoV = V.CodigoV
                                    where U.IdRol = 4";
                dbConnection.Open();
                return Ok(await dbConnection.QueryAsync<object>(sQuery));
            }
        }
        [HttpGet]
        [Route("DetalleUsuario/{id}")]
        public async Task<ActionResult> DetalleUsuario(string id)
        {
                return Ok( await validation.DetalleUsuario(id));

        }
        [HttpGet]
        [Route("DetalleUsuarioConductor/{id}")]
        public async Task<ActionResult> DetalleUsuarioConductor(string id)
        {
             return Ok(await validation.DetalleUsuarioConductor(id));
        }
        [HttpPost]
        [Route("AgregarConductor")]
        public async Task<ActionResult> AgregarConductor(Conductor conductor)
        {

            var vehiculo = await _context.Vehiculos.FindAsync(conductor.CodigoV);
            var id = conductor.CodigoV;
            if(vehiculo is null)
            {
                return NoContent();
            }
            else
            {
                int validar = await validation.Validar(id);
                if(validar >= 1)
                {
                    return NotFound();
                }
                else
                {
                    UsuarioIdentity usu = new UsuarioIdentity()
                    {
                        UserName = conductor.NombreUsuario,
                        Email = conductor.Email,
                        Nombre = conductor.Nombre,
                        IdEstado = 1,
                        IdRol = 4,
                        Apellido = conductor.Apellido,
                        Celular = conductor.Celular,
                        Direccion = conductor.Direccion,
                        IdGenero = conductor.IdGenero,
                        IdTipoDocumento = conductor.IdTipoDocumento,
                        NumeroDocumento = conductor.NumeroDocumento
                    };
                    try
                    {
                        var result = await _userManager
                            .CreateAsync(usu, conductor.Password).ConfigureAwait(false);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    InfoConductore infoConductore = new InfoConductore()
                    {
                        CodigoV = conductor.CodigoV,
                        IdConductor = usu.Id,
                        FotoConductor = Path.GetFileName(conductor.FotoConductor),
                        FechaFin = null,
                        FechaInicio = DateTime.Now,
                    };
                    _context.InfoConductores.Add(infoConductore);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

            }
        }
        [HttpPut]
        [Route("EditarConductor/{id}")]
        public async Task<ActionResult> EditarConductor(string id, Conductor conductor)
        {
            if (id is null)
            {
                return NotFound();
            }
            var usuario = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (usuario is null)
            {
                return NoContent();
            }
            UsuarioIdentity usu = new UsuarioIdentity()
            {
                UserName = conductor.NombreUsuario,
                Email = conductor.Email,
                Nombre = conductor.Nombre,
                IdEstado = 1,
                IdRol = conductor.IdRol,
                Apellido = conductor.Apellido,
                Celular = conductor.Celular,
                Direccion = conductor.Direccion,
                IdGenero = conductor.IdGenero,
                IdTipoDocumento = conductor.IdTipoDocumento,
                NumeroDocumento = conductor.NumeroDocumento,
                PasswordHash = usuario.PasswordHash
            };
            var result = await _userManager
                    .UpdateAsync(usu).ConfigureAwait(false);
            InfoConductore infoConductore = new InfoConductore()
            {
                CodigoV = conductor.CodigoV,
                IdConductor = conductor.IdConductor,
                FotoConductor = Path.GetFileName(conductor.FotoConductor),
                FechaFin = conductor.FechaFin,
                FechaInicio = conductor.FechaInicio,
            };
            _context.InfoConductores.Update(infoConductore);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("EditarDetalle/{id}")]
        public async Task<Object> DetalleEditarCliPro(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"select * from UsuariosIdentity u where u.Id = @id";
                dbConnection.Open();
                var usuario =  await dbConnection.QueryAsync<UsuarioEditar>(sQuery, new { Id = id });
                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    return BadRequest(new { mensaje = "No se encuentra el usuario" });
                }
            }
        }
        [HttpGet]
        [Route("EditarDetalleConductor/{id}")]
        public async Task<Object> DetalleEditarConductor(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            InfoConductore info = await _context.InfoConductores.Where(b => b.IdConductor.Contains(usuario.Id)).FirstAsync();
            if (usuario != null)
            {
                Conductor conductor = new Conductor()
                {
                    IdConductor = info.IdConductor,
                    IdEstado = usuario.IdEstado,
                    Apellido = usuario.Apellido,
                    Nombre = usuario.Nombre,
                    Celular = usuario.Celular,
                    CodigoV = info.CodigoV,
                    Direccion = usuario.Direccion,
                    id = usuario.Id,
                    Email = usuario.Email,
                    FechaFin = info.FechaFin,
                    FechaInicio = info.FechaInicio,
                    FotoConductor = info.FotoConductor,
                    IdGenero = usuario.IdGenero,
                    IdInfo = info.IdInfo,
                    IdRol = usuario.IdRol,
                    IdTipoDocumento = usuario.IdTipoDocumento,
                    NombreUsuario = usuario.UserName,
                    NumeroDocumento = usuario.NumeroDocumento
                };
                return conductor;
            }
            else
            {
                return BadRequest(new { mensaje = "No se encuentra el usuario" });
            }
        }
        [HttpPut]
        [Route("EditarClieProp/{id}")]
        public async Task<ActionResult> EditarCliPro(string id, UsuarioModel usuarioModel)
        {
            if (id is null)
            {
                return NotFound();
            }
            var usuario = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (usuario is null)
            {
                return NoContent();
            }
            UsuarioIdentity usu = new UsuarioIdentity()
            {
                UserName = usuarioModel.NombreUsuario,
                Email = usuarioModel.Email,
                Nombre = usuarioModel.Nombre,
                IdEstado = 1,
                IdRol = usuarioModel.IdRol,
                Apellido = usuarioModel.Apellido,
                Celular = usuarioModel.Celular,
                Direccion = usuarioModel.Direccion,
                IdGenero = usuarioModel.IdGenero,
                IdTipoDocumento = usuarioModel.IdTipoDocumento,
                NumeroDocumento = usuarioModel.NumeroDocumento,
                PasswordHash = usuario.PasswordHash
            };
            var result = await _userManager
                    .UpdateAsync(usu).ConfigureAwait(false);
            return Ok();
        }
        [HttpPut]
        [Route("CambiarEstado/{id}")]
        public async Task<ActionResult> CambiarEstado(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var usuario = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (usuario is null)
            {
                return NoContent();
            }
            if (usuario.IdEstado == 1)
            {
                int? estado = 2;
                usuario.IdEstado = estado.Value;
                _context.UsuariosIdentity.Update(usuario);
            }
            else if (usuario.IdEstado == 2)
            {
                int? estado = 1;
                usuario.IdEstado = estado.Value;
                _context.UsuariosIdentity.Update(usuario);
            }
            else if (usuario.IdEstado == 3)
            {
                int? estado = 1;
                usuario.IdEstado = estado.Value;
                _context.UsuariosIdentity.Update(usuario);
            }
            var result = await _userManager
                    .UpdateAsync(usuario).ConfigureAwait(false);
            return Ok();
        }
        [HttpPut]
        [Route("CambiarEstadoConductor/{id}")]
        public async Task<ActionResult> CambiarEstadoConductor(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var usuario = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
           
            if (usuario is null)
            {
                return NoContent();
            }
            var Query = await _context.InfoConductores.Join(_context.Servicios, x => x.IdInfo, s => s.IdConductor, (x, s) => new { x, s }).Where(c => c.x.IdConductor == usuario.Id && c.s.IdEstadoServicio == 1).CountAsync();
            if(Query >= 1)
            {
                return NotFound();
            }
            else
            {
                if (usuario.IdEstado == 1)
                {
                    int? estado = 2;
                    usuario.IdEstado = estado.Value;
                    _context.UsuariosIdentity.Update(usuario);
                }
                else if (usuario.IdEstado == 2)
                {
                    int? estado = 1;
                    usuario.IdEstado = estado.Value;
                    _context.UsuariosIdentity.Update(usuario);
                }
                else if (usuario.IdEstado == 3)
                {
                    int? estado = 1;
                    usuario.IdEstado = estado.Value;
                    _context.UsuariosIdentity.Update(usuario);
                }
                var result = await _userManager
                        .UpdateAsync(usuario).ConfigureAwait(false);
                return Ok();
            }
        }
    }
}
