using Compartido.DTOs.Eventos;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class AtletaEventoBuscar : IAtletaEventoBuscar
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public AtletaEventoBuscar(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public DTOAtletaEventoListado Ejecutar(int idEvento, int idAtleta)
        {
            return MapperEvento.AtletaEventoToDTOAtletaEventoListado(
                RepoEvento.FindParticipacionByEventoIdAndAtletaId(idEvento, idAtleta)
            );
        }
    }
}
