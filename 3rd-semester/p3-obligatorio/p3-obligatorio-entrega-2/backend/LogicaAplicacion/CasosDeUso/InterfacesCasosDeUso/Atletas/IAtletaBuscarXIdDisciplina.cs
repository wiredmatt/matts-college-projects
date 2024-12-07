using Compartido.DTOs.Atletas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas
{
    public interface IAtletaBuscarXIdDisciplina
    {
        IEnumerable<DTOAtletaListado> Ejecutar(int idDisciplina);
    }
}
