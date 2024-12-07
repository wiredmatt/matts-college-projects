using Compartido.DTOs.Disciplinas;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
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

        public void Ejecutar(DTODisciplinaAlta dtoDisciplina)
        {
            RepoDisciplina.Add(MapperDisciplina.DTODisciplinaAltaToDisciplina(dtoDisciplina));
        }
    }
}
