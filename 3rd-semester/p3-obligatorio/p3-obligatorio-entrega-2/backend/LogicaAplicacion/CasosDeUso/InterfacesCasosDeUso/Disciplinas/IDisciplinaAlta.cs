using Compartido.DTOs.Disciplinas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas
{
    public interface IDisciplinaAlta
    {
        DTODisciplinaListado Ejecutar(DTODisciplinaAlta dtoDisciplina, string emailUsuario);
    }
}
