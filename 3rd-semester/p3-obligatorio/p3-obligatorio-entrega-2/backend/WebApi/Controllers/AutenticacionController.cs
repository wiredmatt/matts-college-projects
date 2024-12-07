using Compartido.DTOs.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;
using Microsoft.AspNetCore.Mvc;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller
    {
        public IUsuarioLogin UsuarioLogin { get; set; }

        public AutenticacionController(IUsuarioLogin usuarioLogin)
        {
            UsuarioLogin = usuarioLogin;
        }

        /// <summary>
        /// Autentica a un usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DTOUsuarioLogeadoConToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Login")]
        public ActionResult Login(DTOUsuarioLogin dtoLogin)
        {
            try 
            {
                DTOUsuarioLogin dtoUsuarioLogin = new DTOUsuarioLogin()
                {
                    Email = dtoLogin.Email,
                    Contrasena = dtoLogin.Contrasena,
                };
                DTOUsuarioLogeado usuario = UsuarioLogin.Ejecutar(dtoUsuarioLogin);

                DTOUsuarioLogeadoConToken usuarioConToken = new DTOUsuarioLogeadoConToken()
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Rol = usuario.Rol,
                    Token = ManejadorToken.CrearToken(usuario)
                };

                return Ok(usuarioConToken);
            }
            catch (ExceptionUsuario ex)
            {
                return BadRequest(new {error = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }
        }
    }
}
