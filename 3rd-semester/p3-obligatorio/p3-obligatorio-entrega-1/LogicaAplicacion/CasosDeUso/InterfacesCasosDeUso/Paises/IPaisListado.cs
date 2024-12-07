using Compartido.DTOs.Paises;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Paises
{
    public interface IPaisListado
    {
        IEnumerable<DTOPaisListado> Ejecutar();
    }
}