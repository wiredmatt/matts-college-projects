using Compartido.DTOs.Atletas;
using LibreriaWeb.Models.Atletas;
using LibreriaWeb.Models.Disciplinas;
using LibreriaWeb.Models.Eventos;
using LibreriaWeb.Models.Paises;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Enums;
using LogicaNegocio.ExcepcionesEntidades.Atletas;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaWeb.Controllers
{
    public class AtletaController : Controller
    {
        public IAtletaListado AtletaListado { get; set; }
        public IAtletaEditar AtletaEditar { get; set; }
        public IAtletaBuscar AtletaBuscar { get; set; }
        public IDisciplinaListado DisciplinaListado { get; set; }
        public IEventoBuscarIdAtleta EventoBuscarIdAtleta { get; set; }


        public AtletaController(
            IAtletaListado atletaListado,
            IAtletaEditar atletaEditar,
            IAtletaBuscar atletaBuscar,
            IDisciplinaListado disciplinaListado,
            IEventoBuscarIdAtleta eventoBuscarIdAtleta
        )
        {
            AtletaListado = atletaListado;
            AtletaEditar = atletaEditar;
            AtletaBuscar = atletaBuscar;
            DisciplinaListado = disciplinaListado;
            EventoBuscarIdAtleta = eventoBuscarIdAtleta;
        }

        // GET: AtletaController
        public ActionResult Index()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<ViewModelAtletaListado> vmAtletaListado =
                AtletaListado.Ejecutar().Select(a => new ViewModelAtletaListado()
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Sexo = a.Sexo,
                    Pais = new ViewModelPaisListado()
                    {
                        Id = a.Pais.Id,
                        Nombre = a.Pais.Nombre,
                        CantidadHabitantes = a.Pais.CantidadHabitantes,
                        NombreDelegado = a.Pais.NombreDelegado,
                        TelefonoDelegado = a.Pais.TelefonoDelegado
                    },
                    Disciplinas = a.Disciplinas.Select(d => new ViewModelDisciplinaListado()
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        Ano = d.Ano
                    })
                }).ToList();

            return View(vmAtletaListado);
        }

        // GET: AtletaController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewModelAtletaEditar vmAtletaEditar = new ViewModelAtletaEditar();
            IEnumerable<ViewModelDisciplinaListado> disciplinas = new List<ViewModelDisciplinaListado>() { };

            try
            {
                DTOAtletaListado atl = AtletaBuscar.Ejecutar(id);
                vmAtletaEditar.Id = atl.Id;
                vmAtletaEditar.Nombre = atl.Nombre;
                vmAtletaEditar.Apellido = atl.Apellido;
                vmAtletaEditar.Sexo = atl.Sexo;
                vmAtletaEditar.Pais = new ViewModelPaisListado()
                {
                    Id = atl.Pais.Id,
                    Nombre = atl.Pais.Nombre,
                    CantidadHabitantes = atl.Pais.CantidadHabitantes,
                    NombreDelegado = atl.Pais.NombreDelegado,
                    TelefonoDelegado = atl.Pais.TelefonoDelegado
                };
                vmAtletaEditar.Disciplinas = atl.Disciplinas.Select(d => new ViewModelDisciplinaListado()
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    Ano = d.Ano
                });

                disciplinas = DisciplinaListado.Ejecutar().Select(d => new ViewModelDisciplinaListado()
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    Ano = d.Ano
                });
            }
            catch (ExceptionAtleta ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Datos incorrectos";
            }

            ViewBag.DisciplinaListado = disciplinas;
            return View(vmAtletaEditar);
        }

        // POST: AtletaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ViewModelAtletaEditar vmAtletaEditar, List<int> selectedDisciplinas)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                DTOAtletaEditar dtoAtleta = new DTOAtletaEditar()
                {
                    // for now the requirement is to only edit Disciplinas, no need for these
                    // Nombre = vmAtletaEditar.Nombre,
                    // Apellido = vmAtletaEditar.Apellido,
                    // Sexo = vmAtletaEditar.Sexo,
                    // IdPais = vmAtletaEditar.Pais.Id,
                    IdsDisciplinas = selectedDisciplinas
                };
                AtletaEditar.Ejecutar(dtoAtleta, id);
                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionAtleta ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            IEnumerable<ViewModelDisciplinaListado> disciplinas = DisciplinaListado.Ejecutar().Select(d => new ViewModelDisciplinaListado()
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Ano = d.Ano
            });
            ViewBag.DisciplinaListado = disciplinas;

            return View(vmAtletaEditar);
        }

        public IEnumerable<ViewModelEventoListadoAPI> Eventos(int id)
        {
            IEnumerable<ViewModelEventoListadoAPI> vmEventoListado = EventoBuscarIdAtleta.Ejecutar(id)
            .Select(e => new ViewModelEventoListadoAPI()
            {
                Id = e.Id,
                Nombre = e.Nombre,
                FechaInicio = e.FechaInicio,
                FechaFin = e.FechaFin,
                Disciplina = new ViewModelDisciplinaListadoAPI()
                {
                    Id = e.Disciplina.Id,
                    Nombre = e.Disciplina.Nombre,
                    Ano = e.Disciplina.Ano
                }
            })
            .ToList();

            return vmEventoListado;
        }
    }
}