using Compartido.DTOs.Atletas;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas
{
    public class AtletaEditar : IAtletaEditar
    {
        public IRepositorioAtleta RepoAtleta { get; set; }
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public AtletaEditar(IRepositorioAtleta repoAtleta, IRepositorioDisciplina repoDisciplina)
        {
            RepoAtleta = repoAtleta;
            RepoDisciplina = repoDisciplina;
        }

        public void Ejecutar(DTOAtletaEditar dtoAtleta, int id)
        {
            Atleta atleta = RepoAtleta.FindById(id);
            atleta.Disciplinas = dtoAtleta.IdsDisciplinas.Select(RepoDisciplina.FindById).ToList();

            RepoAtleta.Update(atleta, id);
        }
    }
}
