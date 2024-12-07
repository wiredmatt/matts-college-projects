using Compartido.DTOs.Usuarios;
using LibreriaWeb.Models.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IUsuarioAlta UsuarioAlta { get; set; }
        public IUsuarioListado UsuarioListado { get; set; }
        public IUsuarioBaja UsuarioBaja { get; set; }
        public IUsuarioBuscar UsuarioBuscar { get; set; }
        public IUsuarioEditar UsuarioEditar { get; set; }

        public UsuarioController(
            IUsuarioAlta usuarioAlta,
            IUsuarioListado usuarioListado,
            IUsuarioBaja usuarioBaja,
            IUsuarioBuscar usuarioBuscar,
            IUsuarioEditar usuarioEditar
        )
        {
            UsuarioAlta = usuarioAlta;
            UsuarioListado = usuarioListado;
            UsuarioBaja = usuarioBaja;
            UsuarioBuscar = usuarioBuscar;
            UsuarioEditar = usuarioEditar;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<ViewModelUsuarioListado> vmUsuarioListado =
                UsuarioListado.Ejecutar().Select(p => new ViewModelUsuarioListado()
                {
                    Id = p.Id,
                    Email = p.Email,
                    Rol = p.Rol
                }).ToList();
            return View(vmUsuarioListado);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewModelUsuarioListado vmUsuarioListado = new ViewModelUsuarioListado();
            try
            {
                DTOUsuarioListado usuario = UsuarioBuscar.Ejecutar(id);

                vmUsuarioListado.Id = usuario.Id;
                vmUsuarioListado.Email = usuario.Email;
                vmUsuarioListado.Rol = usuario.Rol;
            }
            catch (ExceptionUsuario ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Datos incorrectos";
            }
            return View(vmUsuarioListado);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelUsuarioAlta vmUsuarioAlta)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DTOUsuarioAlta dtoUsuarioAlta = new DTOUsuarioAlta()
                    {
                        Email = vmUsuarioAlta.Email,
                        Contrasena = vmUsuarioAlta.Contrasena,
                        Rol = vmUsuarioAlta.Rol,
                        IdCreador = (int)HttpContext.Session.GetInt32("IdUsuario")!,
                    };
                    UsuarioAlta.Ejecutar(dtoUsuarioAlta);
                    return RedirectToAction(nameof(Index));
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

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            // Check if the user has the required role
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewModelUsuarioEditar usuarioVm = new ViewModelUsuarioEditar();
            try
            {
                // Fetch the user details
                DTOUsuarioListado usu = UsuarioBuscar.Ejecutar(id);

                // Map the DTO to the ViewModel
                usuarioVm.Id = usu.Id;
                usuarioVm.Email = usu.Email;
                usuarioVm.Rol = usu.Rol;
            }
            catch (ExceptionUsuario ex)
            {
                // Handle specific user-related exceptions
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                ViewBag.Mensaje = "Datos incorrectos";
            }

            // Return the view with the ViewModel
            return View(usuarioVm);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ViewModelUsuarioEditar usuVM)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                DTOUsuarioEditar usuarioDTO = new DTOUsuarioEditar()
                {
                    Email = usuVM.Email,
                    Contrasena = usuVM.Contrasena,
                    Rol = usuVM.Rol,
                };
                UsuarioEditar.Ejecutar(usuarioDTO, id);

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionUsuario ex)
            {
                ViewBag.Mensaje = ex.Message;

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Datos incorrectos";
            }
            return View(usuVM);
        }

        public ActionResult Delete(int id)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewModelUsuarioListado usuarioVM = new ViewModelUsuarioListado();
            try
            {
                DTOUsuarioListado usuario = UsuarioBuscar.Ejecutar(id);
                usuarioVM.Id = usuario.Id;
                usuarioVM.Email = usuario.Email;
                usuarioVM.Rol = usuario.Rol;
            }
            catch (ExceptionUsuario e)
            {
                ViewBag.Mensaje = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = "Error";
            }


            return View(usuarioVM);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioBaja.Ejecutar(new DTOUsuarioBaja { Id = id });
                    return RedirectToAction(nameof(Index));
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
    }
}