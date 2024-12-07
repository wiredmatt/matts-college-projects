using Compartido.DTOs.Disciplinas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas
{
    public class DisciplinaListado : IDisciplinaListado
    {
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public DisciplinaListado(IRepositorioDisciplina repoDisciplina)
        {
            RepoDisciplina = repoDisciplina;
        }

        public IEnumerable<DTODisciplinaListado> Ejecutar()
        {
            List<Disciplina> Disciplinas = RepoDisciplina.FindAllOrdered().ToList();
            return MapperDisciplina.ListDisciplinaToListDTODisciplinaListado(Disciplinas);
        }
    }
}