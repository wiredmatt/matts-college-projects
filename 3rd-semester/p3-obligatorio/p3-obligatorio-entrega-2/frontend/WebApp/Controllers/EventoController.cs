using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Eventos;
using WebApp.Services;

namespace WebApp.Controllers;

public class EventoController : Controller
{
    private readonly ILogger<EventoController> _logger;

    public EventoController(ILogger<EventoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Buscar(
        [FromQuery] int? idDisciplina = null,
        [FromQuery] DateOnly? fechaInicio = null,
        [FromQuery] DateOnly? fechaFin = null,
        [FromQuery] string? nombreEvento = null,
        [FromQuery] double? puntajeMinimo = null,
        [FromQuery] double? puntajeMaximo = null)
    {
        IEnumerable<ViewModelEventoListado> eventos = new List<ViewModelEventoListado>();
        string bearerToken = HttpContext.Session.GetString("Token") ?? "";

        try
        {
            eventos = EventoService.EventoBuscarPorFiltros(idDisciplina, 
                                                           fechaInicio, 
                                                           fechaFin, 
                                                           nombreEvento, 
                                                           puntajeMinimo, 
                                                           puntajeMaximo, 
                                                           bearerToken);
        } catch (CustomHTTPException e) {
            ViewBag.Mensaje = e.Error;
        }
        catch (Exception e)
        {
            ViewBag.Mensaje = e.Message;
        }

        ViewBag.IdDisciplina = idDisciplina;
        ViewBag.FechaInicio = fechaInicio;
        ViewBag.FechaFin = fechaFin;
        ViewBag.NombreEvento = nombreEvento;
        ViewBag.PuntajeMinimo = puntajeMinimo;
        ViewBag.PuntajeMaximo = puntajeMaximo;

        return View(eventos);
    }
}