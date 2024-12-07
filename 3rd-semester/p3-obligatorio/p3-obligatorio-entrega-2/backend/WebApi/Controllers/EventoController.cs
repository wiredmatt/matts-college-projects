using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.ExcepcionesEntidades.Eventos;
using Compartido.DTOs.Eventos;
using WebApi.Token;
using LogicaNegocio.Enums;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public IEventoBuscarFiltros EventoBuscarFiltros { get; set; }

        public EventoController(IEventoBuscarFiltros eventoBuscarFiltros)
        {
            EventoBuscarFiltros = eventoBuscarFiltros;
        }

        /// <summary>
        /// Permite obtener los Eventos segun los filtros proporcionados.
        /// Si no se proporciona ningun filtro, se devuelven todos los eventos.
        /// </summary>
        /// <param name="idDisciplina"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="nombreEvento"></param>
        /// <param name="puntajeMinimo"></param>
        /// <param name="puntajeMaximo"></param>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<DTOEventoListado>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get(
            [FromQuery] int? idDisciplina = null,
            [FromQuery] string? fechaInicio = null,
            [FromQuery] string? fechaFin = null,
            [FromQuery] string? nombreEvento = null,
            [FromQuery] double? puntajeMinimo = null,
            [FromQuery] double? puntajeMaximo = null)
        {
            DatosToken claims;
            // Todas las funcionalidades que requieran autenticación serán realizadas por un usuario con rol Digitador
            try { 
                claims = ClaimsHelper.GetTokenClaims(HttpContext);
                Checks.CheckRolUsuario(RolUsuario.Digitador, claims.Role); 
            } 
            catch (Exception ex) { return StatusCode(403, new {error = ex.Message}); }

            DateOnly? fechaInicioParsed;
            DateOnly? fechaFinParsed;

            try
            {
                fechaInicioParsed = string.IsNullOrEmpty(fechaInicio) ? null : DateOnly.Parse(fechaInicio);
                fechaFinParsed = string.IsNullOrEmpty(fechaFin) ? null : DateOnly.Parse(fechaFin);
            }
            catch (FormatException)
            {
                return BadRequest(new { error = "Formato de fecha invalido" });
            }
            catch (OverflowException)
            {
                return BadRequest(new { error = "Fecha fuera de rango" });
            }

            try
            {
                return Ok(EventoBuscarFiltros.Ejecutar(idDisciplina,
                                                       fechaInicioParsed,
                                                       fechaFinParsed,
                                                       nombreEvento,
                                                       puntajeMinimo,
                                                       puntajeMaximo));
            }
            catch (ExceptionEvento e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en los datos");
            }
        }
    }
}