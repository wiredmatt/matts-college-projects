using Compartido.DTOs.Disciplinas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas
{
    public class DisciplinaEditar : IDisciplinaEditar
    {
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public DisciplinaEditar(IRepositorioDisciplina repoDisciplina)
        {
            RepoDisciplina = repoDisciplina;
        }

        public DTODisciplinaListado Ejecutar(DTODisciplinaEditar dtoDisciplina, int id, string emailUsuario)
        {
            Disciplina d = RepoDisciplina.Update(MapperDisciplina.DTODisciplinaEditarToDisciplina(dtoDisciplina), id, emailUsuario);
            return MapperDisciplina.DisciplinaToDTODisciplinaListado(d);
        }
    }
}
