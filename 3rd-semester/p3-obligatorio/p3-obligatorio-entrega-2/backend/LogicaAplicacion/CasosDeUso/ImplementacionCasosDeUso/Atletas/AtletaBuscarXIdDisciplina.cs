using Compartido.DTOs.Atletas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas
{
    public class AtletaBuscarXIdDisciplina : IAtletaBuscarXIdDisciplina
    {
        public IRepositorioAtleta RepoAtleta { get; set; }

        public AtletaBuscarXIdDisciplina(IRepositorioAtleta repoAtleta)
        {
            RepoAtleta = repoAtleta;
        }

        public IEnumerable<DTOAtletaListado> Ejecutar(int idDisciplina)
        {
            IEnumerable<Atleta> atletas = RepoAtleta.FindByIdDisciplina(idDisciplina);
            return MapperAtleta.ListAtletaToListDTOAtletaListado(atletas);
        }
    }
}
