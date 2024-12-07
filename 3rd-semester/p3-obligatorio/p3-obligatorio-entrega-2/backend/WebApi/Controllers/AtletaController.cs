using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.ExcepcionesEntidades.Atletas;
using Compartido.DTOs.Atletas;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtletaController : ControllerBase
    {
        public IAtletaBuscarXIdDisciplina AtletaBuscarXIdDisciplina { get; set; }

        public AtletaController(IAtletaBuscarXIdDisciplina buscarAtletasXDisciplina)
        {
            AtletaBuscarXIdDisciplina = buscarAtletasXDisciplina;
        }

        /// <summary>
        /// Obtiene los atletas que practican una disciplina dada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorIdDisciplina/{id}")]
        [ProducesResponseType(typeof(DTOAtletaListado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(AtletaBuscarXIdDisciplina.Ejecutar(id));
            }
            catch (ExceptionAtleta e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e.Message});
            }
        }
    }
}