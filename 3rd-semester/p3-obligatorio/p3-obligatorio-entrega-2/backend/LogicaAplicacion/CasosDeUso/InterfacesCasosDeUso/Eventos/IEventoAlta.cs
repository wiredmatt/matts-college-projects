using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoAlta
    {
        DTOEventoListado Ejecutar(DTOEventoAlta dtoEvento);
    }
}
