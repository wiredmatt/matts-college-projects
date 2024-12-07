using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class EventoBuscarId : IEventoBuscarId
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public EventoBuscarId(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public DTOEventoListado Ejecutar(int id)
        {
            Evento Evento = RepoEvento.FindById(id);
            return MapperEvento.EventoToDTOEventoListado(Evento);
        }
    }
}
