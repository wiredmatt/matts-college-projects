using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoAlta
    {
        void Ejecutar(DTOEventoAlta dtoEvento);
    }
}
