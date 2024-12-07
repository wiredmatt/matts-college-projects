using Compartido.DTOs.Disciplinas;
using LibreriaWeb.Models.Disciplinas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Enums;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaWeb.Controllers
{
    public class DisciplinaController : Controller
    {
        public IDisciplinaAlta DisciplinaAlta { get; set; }
        public IDisciplinaListado DisciplinaListado { get; set; }

        public DisciplinaController(IDisciplinaAlta disciplinaAlta, IDisciplinaListado disciplinaListado)
        {
            DisciplinaAlta = disciplinaAlta;
            DisciplinaListado = disciplinaListado;
        }


        // GET: DisciplinaController
        public ActionResult Index()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<ViewModelDisciplinaListado> vmDisciplinaListado =
                DisciplinaListado.Ejecutar().Select(p => new ViewModelDisciplinaListado()
                {
                    Nombre = p.Nombre,
                    Id = p.Id,
                    Ano = p.Ano,
                }).ToList();
            return View(vmDisciplinaListado);
        }

        // GET: DisciplinaController/Create
        public ActionResult Create()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: DisciplinaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelDisciplinaAlta vmDisciplinaAlta)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DTODisciplinaAlta dtoDisciplinaAlta = new DTODisciplinaAlta()
                    {
                        Nombre = vmDisciplinaAlta.Nombre,
                        Id = vmDisciplinaAlta.Id,
                        Ano = vmDisciplinaAlta.Ano,
                    };
                    DisciplinaAlta.Ejecutar(dtoDisciplinaAlta);
                    return RedirectToAction(nameof(Index));
                }
                catch (ExceptionDisciplina ex)
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
