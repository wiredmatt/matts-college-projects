using Compartido.DTOs.Atletas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas
{
    public interface IAtletaListado
    {
        IEnumerable<DTOAtletaListado> Ejecutar();
    }
}