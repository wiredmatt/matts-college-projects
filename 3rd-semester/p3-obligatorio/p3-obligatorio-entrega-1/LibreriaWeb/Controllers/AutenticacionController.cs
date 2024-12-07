using Compartido.DTOs.Usuarios;
using LibreriaWeb.Models.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaWeb.Controllers
{
    public class AutenticacionController : Controller
    {
        public IUsuarioLogin UsuarioLogin { get; set; }

        public AutenticacionController(IUsuarioLogin usuarioLogin)
        {
            UsuarioLogin = usuarioLogin;
        }

        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewModelUsuarioLogin vmUsuarioLogin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DTOUsuarioLogin dtoUsuarioLogin = new DTOUsuarioLogin()
                    {
                        Email = vmUsuarioLogin.Email,
                        Contrasena = vmUsuarioLogin.Contrasena,
                    };
                    DTOUsuarioLogeado usuario = UsuarioLogin.Ejecutar(dtoUsuarioLogin);

                    HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
                    HttpContext.Session.SetString("RolUsuario", usuario.Rol.ToString());

                    return Redirect("/");
                }
                catch (ExceptionUsuario ex)
                {
                    ViewBag.Mensaje = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
