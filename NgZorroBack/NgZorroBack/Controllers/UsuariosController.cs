using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NgZorroBack.Models;
using NgZorroBack.Usuarios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public UsuariosController(UserManager<UsuarioIdentity> userManager,
            SignInManager<UsuarioIdentity> signInManager, IOptions<ConfiguracionGlobal> configuracionGlobal, MerakiZorroContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuracionGlobal = configuracionGlobal.Value;
            _context = context;
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
                IdEstado = usuarioModel.IdEstado,
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
                return Ok(result);
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
        [Route("Perfil")]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<object> ObtenerPerfilUsuario()
        {
            string usuarioId = User.Claims.First(c => c.Type == "UsuarioID").Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId).ConfigureAwait(false);

            if (usuario != null)
            {
                HttpContext.Session.SetString(SessionKeyName, usuario.Id);
                var vergil = HttpContext.Session.GetString("IdUsuario");
                return new
                {
                    usuario.Nombre,
                    usuario.Email,
                    usuario.UserName,
                    usuario.IdEstado,
                    usuario.IdRol,
                    usuario.NumeroDocumento,
                    usuario.Apellido,
                    usuario.Celular,
                    usuario.Direccion,
                };
            }
            else
            {
                return BadRequest(new { mensaje = "No se encuentra el usuario" });
            }
        }
    }
}
