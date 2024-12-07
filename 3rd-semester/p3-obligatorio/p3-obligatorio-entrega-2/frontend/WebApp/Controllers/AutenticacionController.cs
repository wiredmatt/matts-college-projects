using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Usuarios;
using WebApp.Services;

namespace WebApp.Controllers;

public class AutenticacionController : Controller
{
    private readonly ILogger<AtletaController> _logger;

    public AutenticacionController(ILogger<AtletaController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(ViewModelUsuarioLogin vmLogin)
    {
        try
        {
            ViewModelUsuarioLogeadoConToken usuario = AutenticacionService.Login(vmLogin);
            HttpContext.Session.SetString("Token", usuario.Token);
            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetString("RolUsuario", usuario.Rol.ToString());
            HttpContext.Session.SetString("EmailUsuario", usuario.Email.ToString());
            return RedirectToAction("Index", "Home");
        }
        catch (CustomHTTPException e) {
            ViewBag.Mensaje = e.Error;
        }
        catch (Exception e)
        {
            ViewBag.Mensaje = e.Message;
        }

        return View(vmLogin);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}