using Compartido.DTOs.Atletas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas
{
    public interface IAtletaBuscar
    {
        DTOAtletaListado Ejecutar(int id);
    }
}
