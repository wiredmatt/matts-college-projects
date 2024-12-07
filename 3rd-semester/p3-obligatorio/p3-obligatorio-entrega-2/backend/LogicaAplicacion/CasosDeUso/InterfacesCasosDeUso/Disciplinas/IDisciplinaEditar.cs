using Compartido.DTOs.Disciplinas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas
{
    public interface IDisciplinaEditar
    {
        DTODisciplinaListado Ejecutar(DTODisciplinaEditar dtoDisciplina, int id, string emailUsuario);
    }
}
