using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class EventoBuscarFiltros : IEventoBuscarFiltros
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public EventoBuscarFiltros(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public IEnumerable<DTOEventoListado> Ejecutar(int? idDisciplina,
                                                      DateOnly? fechaInicio,
                                                      DateOnly? fechaFin,
                                                      string? nombreEvento,
                                                      double? puntajeMinimo,
                                                      double? puntajeMaximo)
        {
            IEnumerable<Evento> todos = RepoEvento.FindAll();

            IEnumerable<Evento> porDisciplinaId = idDisciplina == null ? todos : RepoEvento.FindByIdDisciplina((int)idDisciplina);
            IEnumerable<Evento> entreFechas = (fechaInicio == null && fechaFin == null) ? todos : RepoEvento.FindByDateRange(fechaInicio, fechaFin);
            IEnumerable<Evento> porNombreEvento = (nombreEvento == null) ? todos : RepoEvento.FindByNameLike(nombreEvento);
            IEnumerable<Evento> porPuntaje = (puntajeMinimo == 0 && puntajeMaximo == 0) ? todos : RepoEvento.FindByPuntajeRange(puntajeMinimo, puntajeMaximo);

            // "Produces the set intersection of two sequences by using the 
            // default equality comparer to compare values"
            // NOTE(matt): Evento implementa IEquatable<Evento> para poder
            // usar .Intersect() a nivel de objeto[], en lugar de .Where() a nivel de ORM.
            IEnumerable<Evento> eventos = todos
                                                .Intersect(porDisciplinaId)
                                                .Intersect(entreFechas)
                                                .Intersect(porNombreEvento)
                                                .Intersect(porPuntaje);

            return MapperEvento.ListEventoToListDTOEventoListado(eventos);
        }
    }
}
