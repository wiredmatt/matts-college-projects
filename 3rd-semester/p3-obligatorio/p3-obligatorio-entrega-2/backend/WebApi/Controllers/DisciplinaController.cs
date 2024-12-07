using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using Compartido.DTOs.Disciplinas;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;
using LogicaNegocio.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using LogicaNegocio.Enums;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        public IDisciplinaAlta DisciplinaAlta { get; set; }
        public IDisciplinaListado DisciplinaListado { get; set; }
        public IDisciplinaBuscar DisciplinaBuscar { get; set; }
        public IDisciplinaEditar DisciplinaEditar { get; set; }
        public IDisciplinaBaja DisciplinaBaja { get; set; }

        public DisciplinaController(IDisciplinaAlta disciplinaAlta,
                                    IDisciplinaListado disciplinaListado,
                                    IDisciplinaBuscar disciplinaBuscar,
                                    IDisciplinaEditar disciplinaEditar,
                                    IDisciplinaBaja disciplinaBaja)
        {
            DisciplinaAlta = disciplinaAlta;
            DisciplinaListado = disciplinaListado;
            DisciplinaBuscar = disciplinaBuscar;
            DisciplinaEditar = disciplinaEditar;
            DisciplinaBaja = disciplinaBaja;
        }

        /// <summary>
        /// Permite obtener todas las Disciplinas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<DTODisciplinaListado>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public IActionResult Get()
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            try
            {
                return Ok(DisciplinaListado.Ejecutar());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }

        }
        /// <summary>
        /// Permite obtener los datos de una Disciplina
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(DTODisciplinaListado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "ObtenerDisciplinaPorId")]
        public IActionResult Get(int id)
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            try
            {
                return Ok(DisciplinaBuscar.Ejecutar(id));
            }
            catch (ExceptionDisciplina ex)
            {
                return NotFound(new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }

        }

        /// <summary>
        /// Permite crear una Disciplina
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(DTODisciplinaListado), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(DTODisciplinaAlta dto)
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            try
            {
                DTODisciplinaListado disciplina = DisciplinaAlta.Ejecutar(dto, claims.Email);
                return CreatedAtRoute("ObtenerDisciplinaPorId", new { Id = disciplina.Id }, disciplina);
            }
            catch (ExceptionDisciplina e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
            catch (ConflictException e)
            {
                return Conflict(new
                {
                    error = e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e.Message});
            }
        }

        /// <summary>
        /// Permite modificar los datos de una Disciplina
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(DTODisciplinaListado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, DTODisciplinaEditar dto)
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            try
            {
                DTODisciplinaListado disciplina = DisciplinaEditar.Ejecutar(dto, id, claims.Email);
                return Ok(disciplina);
            }
            catch (ExceptionDisciplina ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
            catch (ConflictException ex)
            {
                return Conflict(new
                {
                    error = ex.Message
                });
            } catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }

        }

        /// <summary>
        /// Permite eliminar una Disciplina
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            try
            {
                DisciplinaBaja.Ejecutar(id, claims.Email);
                return NoContent();
            }
            catch (ExceptionDisciplina ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }
        }
    }
}