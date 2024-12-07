using Compartido.DTOs.Atletas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Atletas
{
    public class AtletaAlta : IAtletaAlta
    {
        public IRepositorioAtleta RepoAtleta { get; set; }
        public IRepositorioDisciplina RepoDisciplina { get; set; }
        public IRepositorioPais RepoPais { get; set; }


        public AtletaAlta(IRepositorioAtleta repoAtleta,
                          IRepositorioDisciplina repoDisciplina,
                          IRepositorioPais repoPais)
        {
            RepoAtleta = repoAtleta;
            RepoDisciplina = repoDisciplina;
            RepoPais = repoPais;
        }

        public void Ejecutar(DTOAtletaAlta dtoAtleta)
        {
            Atleta atleta = MapperAtleta.DTOAtletaAltaToAtleta(dtoAtleta);

            atleta.Disciplinas = dtoAtleta.IdsDisciplinas.Select(RepoDisciplina.FindById).ToList();
            atleta.Pais = RepoPais.FindById(atleta.IdPais);

            RepoAtleta.Add(atleta);
        }
    }
}
