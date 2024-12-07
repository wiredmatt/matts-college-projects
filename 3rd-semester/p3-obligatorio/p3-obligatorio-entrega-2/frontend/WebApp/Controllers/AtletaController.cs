using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Atletas;
using WebApp.Services;

namespace WebApp.Controllers;

public class AtletaController : Controller
{
    private readonly ILogger<AtletaController> _logger;

    public AtletaController(ILogger<AtletaController> logger)
    {
        _logger = logger;
    }

    public IActionResult BuscarPorDisciplina(int? id)
    {
        IEnumerable<ViewModelAtletaListado> atletas = new List<ViewModelAtletaListado>();

        try
        {
            if (id.HasValue)
                atletas = AtletaService.AtletaBuscarPorIdDisciplina(id.Value);
        } catch (CustomHTTPException e) {
            ViewBag.Mensaje = e.Error;
        }
        catch (Exception e)
        {
            ViewBag.Mensaje = e.Message;
        }

        ViewBag.Id = id;
        return View(atletas);
    }
}