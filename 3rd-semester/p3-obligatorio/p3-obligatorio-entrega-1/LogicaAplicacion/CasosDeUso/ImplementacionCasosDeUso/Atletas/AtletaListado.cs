using Compartido.DTOs.Atletas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas
{
    public class AtletaListado : IAtletaListado
    {
        public IRepositorioAtleta RepoAtleta { get; set; }

        public AtletaListado(IRepositorioAtleta repoAtleta)
        {
            RepoAtleta = repoAtleta;
        }

        public IEnumerable<DTOAtletaListado> Ejecutar()
        {
            List<Atleta> Atletas = RepoAtleta.FindAllOrdered().ToList();
            return MapperAtleta.ListAtletaToListDTOAtletaListado(Atletas);
        }
    }
}