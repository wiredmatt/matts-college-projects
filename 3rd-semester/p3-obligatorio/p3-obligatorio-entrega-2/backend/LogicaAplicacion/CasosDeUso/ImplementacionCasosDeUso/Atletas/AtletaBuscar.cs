using Compartido.DTOs.Atletas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas
{
    public class AtletaBuscar : IAtletaBuscar
    {
        public IRepositorioAtleta RepoAtleta { get; set; }

        public AtletaBuscar(IRepositorioAtleta repoAtleta)
        {
            RepoAtleta = repoAtleta;
        }

        public DTOAtletaListado Ejecutar(int id)
        {
            Atleta Atleta = RepoAtleta.FindById(id);
            return MapperAtleta.AtletaToDTOAtletaListado(Atleta);
        }
    }
}
