using Compartido.DTOs.Disciplinas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas
{
    public class DisciplinaAlta : IDisciplinaAlta
    {
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public DisciplinaAlta(IRepositorioDisciplina repoDisciplina)
        {
            RepoDisciplina = repoDisciplina;
        }

        public DTODisciplinaListado Ejecutar(DTODisciplinaAlta dtoDisciplina, string emailUsuario)
        {
            Disciplina d = RepoDisciplina.Add(MapperDisciplina.DTODisciplinaAltaToDisciplina(dtoDisciplina), emailUsuario);
            return MapperDisciplina.DisciplinaToDTODisciplinaListado(d);
        }
    }
}
