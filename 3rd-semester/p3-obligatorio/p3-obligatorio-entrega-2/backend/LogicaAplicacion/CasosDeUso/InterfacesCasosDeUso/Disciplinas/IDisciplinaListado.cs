using Compartido.DTOs.Disciplinas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas
{
    public interface IDisciplinaListado
    {
        IEnumerable<DTODisciplinaListado> Ejecutar();
    }
}