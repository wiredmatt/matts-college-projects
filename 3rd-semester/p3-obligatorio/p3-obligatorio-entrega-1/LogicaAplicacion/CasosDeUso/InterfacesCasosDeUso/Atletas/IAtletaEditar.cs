using Compartido.DTOs.Atletas;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Atletas
{
    public interface IAtletaEditar
    {
        void Ejecutar(DTOAtletaEditar dtoAtleta, int id);
    }
}
