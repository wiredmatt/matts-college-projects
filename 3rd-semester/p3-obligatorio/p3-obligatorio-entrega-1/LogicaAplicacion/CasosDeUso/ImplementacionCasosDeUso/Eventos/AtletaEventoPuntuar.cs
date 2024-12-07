using Compartido.DTOs.Eventos;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Eventos
{
    public class AtletaEventoPuntuar : IAtletaEventoPuntuar
    {
        public IRepositorioEvento RepoEvento { get; set; }

        public AtletaEventoPuntuar(IRepositorioEvento repoEvento)
        {
            RepoEvento = repoEvento;
        }

        public void Ejecutar(DTOAtletaEventoPuntuar dtoPuntuar)
        {
            RepoEvento.SetAtletaEventoPuntaje(dtoPuntuar.IdEvento, dtoPuntuar.IdAtleta, dtoPuntuar.Puntaje);
        }
    }
}
