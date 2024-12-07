using Compartido.DTOs.Disciplinas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas
{
    public interface IDisciplinaBuscar
    {
        DTODisciplinaListado Ejecutar(int id);
    }
}
