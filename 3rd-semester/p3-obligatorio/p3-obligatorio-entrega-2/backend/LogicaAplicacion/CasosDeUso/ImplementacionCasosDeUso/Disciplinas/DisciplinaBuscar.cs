using Compartido.DTOs.Disciplinas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas
{
    public class DisciplinaBuscar : IDisciplinaBuscar
    {
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public DisciplinaBuscar(IRepositorioDisciplina repoDisciplina)
        {
            RepoDisciplina = repoDisciplina;
        }

        public DTODisciplinaListado Ejecutar(int id)
        {
            Disciplina Disciplina = RepoDisciplina.FindById(id);
            return MapperDisciplina.DisciplinaToDTODisciplinaListado(Disciplina);
        }
    }
}
