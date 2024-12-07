using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class EventoBuscarIdAtleta : IEventoBuscarIdAtleta
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public EventoBuscarIdAtleta(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public IEnumerable<DTOEventoListado> Ejecutar(int idAtleta)
        {
            IEnumerable<Evento> eventos = RepoEvento.FindAllByIdAtleta(idAtleta);
            return MapperEvento.ListEventoToListDTOEventoListado(eventos);
        }
    }
}
