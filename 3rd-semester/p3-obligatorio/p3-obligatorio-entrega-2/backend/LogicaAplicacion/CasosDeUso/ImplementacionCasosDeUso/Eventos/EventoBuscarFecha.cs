using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class EventoBuscarFecha : IEventoBuscarFecha
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public EventoBuscarFecha(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public IEnumerable<DTOEventoListado> Ejecutar(DateOnly fecha)
        {
            IEnumerable<Evento> eventos = RepoEvento.FindByDate(fecha);
            return MapperEvento.ListEventoToListDTOEventoListado(eventos);
        }
    }
}
