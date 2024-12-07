using Compartido.DTOs.Disciplinas;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;

namespace Compartido.Mappers
{
    public class MapperDisciplina
    {

        public static DTODisciplinaListado DisciplinaToDTODisciplinaListado(Disciplina disciplina)
        {
            return new DTODisciplinaListado()
            {
                Id = disciplina.Id,
                Nombre = disciplina.Nombre.Valor,
                Ano = disciplina.Ano
            };
        }


        public static IEnumerable<DTODisciplinaListado> ListDisciplinaToListDTODisciplinaListado(List<Disciplina> Disciplinas)
        {
            return Disciplinas.Select(DisciplinaToDTODisciplinaListado);
        }

        public static Disciplina DTODisciplinaAltaToDisciplina(DTODisciplinaAlta dtoDisciplina)
        {
            if (dtoDisciplina == null)
            {
                throw new ExceptionDisciplina("Datos incorrectos");
            }
            return new Disciplina(dtoDisciplina.Nombre, dtoDisciplina.Id, dtoDisciplina.Ano);
        }

        public static Disciplina DTODisciplinaEditarToDisciplina(DTODisciplinaEditar dtoDisciplina)
        {
            if (dtoDisciplina == null)
            {
                throw new ExceptionDisciplina("Datos incorrectos");
            }
            return new Disciplina(dtoDisciplina.Nombre, dtoDisciplina.Ano);
        }
    }
}
