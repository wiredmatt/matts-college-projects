using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class EventoAlta : IEventoAlta
    {
        public IRepositorioEvento RepoEvento { get; set; }
        public IRepositorioDisciplina RepoDisciplina { get; set; }
        public IRepositorioAtleta RepoAtleta { get; set; }

        public EventoAlta(IRepositorioEvento repoEvento,
                          IRepositorioDisciplina repoDisciplina,
                          IRepositorioAtleta repoAtleta)
        {
            RepoEvento = repoEvento;
            RepoDisciplina = repoDisciplina;
            RepoAtleta = repoAtleta;
        }

        public DTOEventoListado Ejecutar(DTOEventoAlta dtoEvento)
        {
            Evento evento = MapperEvento.DTOEventoAltaToEvento(dtoEvento);

            evento.Disciplina = RepoDisciplina.FindById(dtoEvento.IdDisciplina);
            evento.Atletas = dtoEvento.IdsAtletas.Select(RepoAtleta.FindById).ToList();
            evento.Validar();

            return MapperEvento.EventoToDTOEventoListado(RepoEvento.Add(evento));
        }
    }
}
