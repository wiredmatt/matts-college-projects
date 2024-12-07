using Compartido.DTOs.Eventos;
using LibreriaWeb.Models.Eventos;
using LibreriaWeb.Models.Disciplinas;
using LibreriaWeb.Models.Paises;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Enums;
using LogicaNegocio.ExcepcionesEntidades.Eventos;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LibreriaWeb.Models.Atletas;
using Compartido.DTOs.Atletas;

namespace LibreriaWeb.Controllers
{
    public class EventoController : Controller
    {
        public IEventoAlta EventoAlta { get; set; }
        public IEventoBuscarId EventoBuscarId { get; set; }
        public IEventoBuscarFecha EventoBuscarFecha { get; set; }
        public IAtletaEventoBuscar AtletaEventoBuscar { get; set; }
        public IDisciplinaListado DisciplinaListado { get; set; }
        public IAtletaListado AtletaListado { get; set; }
        public IAtletaBuscar AtletaBuscar { get; set; }
        public IAtletaEventoPuntuar AtletaEventoPuntuar { get; set; }


        public EventoController(
            IEventoAlta eventoAlta,
            IEventoBuscarId eventoBuscarId,
            IEventoBuscarFecha eventoBuscarFecha,
            IAtletaEventoBuscar atletaEventoBuscar,
            IDisciplinaListado disciplinaListado,
            IAtletaListado atletaListado,
            IAtletaBuscar atletaBuscar,
            IAtletaEventoPuntuar atletaEventoPuntuar
        )
        {
            EventoAlta = eventoAlta;
            EventoBuscarId = eventoBuscarId;
            EventoBuscarFecha = eventoBuscarFecha;
            AtletaEventoBuscar = atletaEventoBuscar;
            DisciplinaListado = disciplinaListado;
            AtletaListado = atletaListado;
            AtletaBuscar = atletaBuscar;
            AtletaEventoPuntuar = atletaEventoPuntuar;
        }

        // GET: EventoController
        public ActionResult Index()
        {
            return RedirectToAction("Buscar");
        }

        // GET: EventoController/Create
        public ActionResult Create()
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.DisciplinaListado = DisciplinaListado.Ejecutar().Select(p => new ViewModelDisciplinaListado()
            {
                Nombre = p.Nombre,
                Id = p.Id,
                Ano = p.Ano,
            }).AsEnumerable();

            ViewBag.AtletaListado = AtletaListado.Ejecutar().Select(p => new ViewModelAtletaListado()
            {
                Nombre = p.Nombre,
                Id = p.Id,
                Apellido = p.Apellido,
                Sexo = p.Sexo,
                Pais = new ViewModelPaisListado()
                {
                    Id = p.Pais.Id,
                    Nombre = p.Pais.Nombre,
                    CantidadHabitantes = p.Pais.CantidadHabitantes,
                    NombreDelegado = p.Pais.NombreDelegado,
                    TelefonoDelegado = p.Pais.TelefonoDelegado,
                },
                Disciplinas = p.Disciplinas.Select(d => new ViewModelDisciplinaListado()
                {
                    Nombre = d.Nombre,
                    Id = d.Id,
                    Ano = d.Ano,
                })
            }).AsEnumerable();

            return View();
        }

        // POST: EventoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelEventoAlta vmEventoAlta)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DTOEventoAlta dtoEventoAlta = new DTOEventoAlta()
                    {
                        Nombre = vmEventoAlta.Nombre,
                        IdDisciplina = vmEventoAlta.IdDisciplina,
                        IdsAtletas = vmEventoAlta.IdsAtletas,
                        FechaInicio = vmEventoAlta.FechaInicio,
                        FechaFin = vmEventoAlta.FechaFin,
                    };
                    EventoAlta.Ejecutar(dtoEventoAlta);
                    return RedirectToAction(nameof(Buscar), new { Fecha = vmEventoAlta.FechaInicio });
                }
                catch (ExceptionEvento ex)
                {
                    ViewBag.Mensaje = ex.Message;
                }
            }

            ViewBag.DisciplinaListado = DisciplinaListado.Ejecutar().Select(p => new ViewModelDisciplinaListado()
            {
                Nombre = p.Nombre,
                Id = p.Id,
                Ano = p.Ano,
            }).AsEnumerable();

            ViewBag.AtletaListado = AtletaListado.Ejecutar().Select(p => new ViewModelAtletaListado()
            {
                Nombre = p.Nombre,
                Id = p.Id,
                Apellido = p.Apellido,
                Sexo = p.Sexo,
                Pais = new ViewModelPaisListado()
                {
                    Id = p.Pais.Id,
                    Nombre = p.Pais.Nombre,
                    CantidadHabitantes = p.Pais.CantidadHabitantes,
                    NombreDelegado = p.Pais.NombreDelegado,
                    TelefonoDelegado = p.Pais.TelefonoDelegado,
                },
                Disciplinas = p.Disciplinas.Select(d => new ViewModelDisciplinaListado()
                {
                    Nombre = d.Nombre,
                    Id = d.Id,
                    Ano = d.Ano,
                })
            }).AsEnumerable();

            return View(vmEventoAlta);
        }

        public ActionResult Buscar(ViewModelEventoBuscarFecha vmEventoBuscarFecha)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            vmEventoBuscarFecha.Fecha = vmEventoBuscarFecha.Fecha == default ? DateOnly.FromDateTime(DateTime.Now) : vmEventoBuscarFecha.Fecha;
            IEnumerable<ViewModelEventoListado> vmEventoListado = [];

            if (ModelState.IsValid)
            {
                var eventos = EventoBuscarFecha.Ejecutar(vmEventoBuscarFecha.Fecha);

                vmEventoListado = eventos.Select(e => new ViewModelEventoListado()
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    Disciplina = new ViewModelDisciplinaListado()
                    {
                        Id = e.Disciplina.Id,
                        Nombre = e.Disciplina.Nombre,
                        Ano = e.Disciplina.Ano,
                    },
                }
                );

                ViewBag.Eventos = vmEventoListado;
                return View(vmEventoBuscarFecha);
            }

            ViewBag.Eventos = vmEventoListado;
            return View(vmEventoBuscarFecha);
        }

        public ActionResult Participantes(int id)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var dtoEvento = EventoBuscarId.Ejecutar(id);

                ViewModelEventoListado vmEvento = new ViewModelEventoListado()
                {
                    Id = dtoEvento.Id,
                    Nombre = dtoEvento.Nombre,
                    Disciplina = new ViewModelDisciplinaListado()
                    {
                        Id = dtoEvento.Disciplina.Id,
                        Nombre = dtoEvento.Disciplina.Nombre,
                        Ano = dtoEvento.Disciplina.Ano,
                    },
                    FechaInicio = dtoEvento.FechaInicio,
                    FechaFin = dtoEvento.FechaFin,
                    Atletas = dtoEvento.Atletas.Select(a => new ViewModelAtletaListado()
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
                            TelefonoDelegado = a.Pais.TelefonoDelegado,
                        }
                    }).AsEnumerable()
                };

                return View(vmEvento);
            }
            catch (ExceptionEvento ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        public ActionResult Puntuar([FromQuery] int idEvento, [FromQuery] int idAtleta)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            var dtoParticipacion = AtletaEventoBuscar.Ejecutar(idEvento, idAtleta);

            if (dtoParticipacion == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var vmParticipacion = new ViewModelAtletaEventoListado()
            {
                IdAtleta = dtoParticipacion.IdAtleta,
                IdEvento = dtoParticipacion.IdEvento,
                Puntaje = dtoParticipacion.Puntaje
            };

            DTOAtletaListado dtoAtl = AtletaBuscar.Ejecutar(idAtleta);
            ViewModelAtletaListado vmAtleta = new ViewModelAtletaListado()
            {
                Id = dtoAtl.Id,
                Nombre = dtoAtl.Nombre,
                Apellido = dtoAtl.Apellido,
                Sexo = dtoAtl.Sexo,
                Pais = new ViewModelPaisListado()
                {
                    Id = dtoAtl.Pais.Id,
                    Nombre = dtoAtl.Pais.Nombre,
                    CantidadHabitantes = dtoAtl.Pais.CantidadHabitantes,
                    NombreDelegado = dtoAtl.Pais.NombreDelegado,
                    TelefonoDelegado = dtoAtl.Pais.TelefonoDelegado
                }
            };

            DTOEventoListado dtoEv = EventoBuscarId.Ejecutar(idEvento);
            ViewModelEventoListado vmEvento = new ViewModelEventoListado()
            {
                Id = dtoEv.Id,
                Nombre = dtoEv.Nombre,
                Disciplina = new ViewModelDisciplinaListado()
                {
                    Id = dtoEv.Disciplina.Id,
                    Nombre = dtoEv.Disciplina.Nombre,
                    Ano = dtoEv.Disciplina.Ano
                },
                FechaInicio = dtoEv.FechaInicio,
                FechaFin = dtoEv.FechaFin
            };

            ViewBag.Atleta = vmAtleta;
            ViewBag.Evento = vmEvento;

            return View(vmParticipacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Puntuar(ViewModelAtletaEventoListado vmParticipacion)
        {
            if (!Checks.CheckRolUsuario(RolUsuario.Digitador, HttpContext.Session.GetString("RolUsuario")) &&
                !Checks.CheckRolUsuario(RolUsuario.Administrador, HttpContext.Session.GetString("RolUsuario")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DTOAtletaEventoPuntuar dtoParticipacion = new DTOAtletaEventoPuntuar()
                    {
                        IdAtleta = vmParticipacion.IdAtleta,
                        IdEvento = vmParticipacion.IdEvento,
                        Puntaje = vmParticipacion.Puntaje
                    };

                    AtletaEventoPuntuar.Ejecutar(dtoParticipacion);

                    return RedirectToAction(nameof(Participantes), new { id = vmParticipacion.IdEvento });
                }
                catch (ExceptionEvento ex)
                {
                    ViewBag.Mensaje = ex.Message;
                }
            }

            DTOAtletaListado dtoAtl = AtletaBuscar.Ejecutar(vmParticipacion.IdAtleta);
            ViewModelAtletaListado vmAtleta = new ViewModelAtletaListado()
            {
                Id = dtoAtl.Id,
                Nombre = dtoAtl.Nombre,
                Apellido = dtoAtl.Apellido,
                Sexo = dtoAtl.Sexo,
                Pais = new ViewModelPaisListado()
                {
                    Id = dtoAtl.Pais.Id,
                    Nombre = dtoAtl.Pais.Nombre,
                    CantidadHabitantes = dtoAtl.Pais.CantidadHabitantes,
                    NombreDelegado = dtoAtl.Pais.NombreDelegado,
                    TelefonoDelegado = dtoAtl.Pais.TelefonoDelegado
                }
            };

            DTOEventoListado dtoEv = EventoBuscarId.Ejecutar(vmParticipacion.IdEvento);
            ViewModelEventoListado vmEvento = new ViewModelEventoListado()
            {
                Id = dtoEv.Id,
                Nombre = dtoEv.Nombre,
                Disciplina = new ViewModelDisciplinaListado()
                {
                    Id = dtoEv.Disciplina.Id,
                    Nombre = dtoEv.Disciplina.Nombre,
                    Ano = dtoEv.Disciplina.Ano
                },
                FechaInicio = dtoEv.FechaInicio,
                FechaFin = dtoEv.FechaFin
            };

            ViewBag.Atleta = vmAtleta;
            ViewBag.Evento = vmEvento;

            return View(vmParticipacion);
        }
    }
}