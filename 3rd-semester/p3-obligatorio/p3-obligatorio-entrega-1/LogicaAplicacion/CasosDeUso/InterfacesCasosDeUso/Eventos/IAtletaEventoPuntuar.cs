using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IAtletaEventoPuntuar
    {
        void Ejecutar(DTOAtletaEventoPuntuar dtoPuntuar);
    }
}
