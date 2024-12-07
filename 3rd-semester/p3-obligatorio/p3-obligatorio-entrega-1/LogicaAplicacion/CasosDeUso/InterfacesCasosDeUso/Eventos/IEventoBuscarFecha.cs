using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoBuscarFecha
    {
        IEnumerable<DTOEventoListado> Ejecutar(DateOnly fecha);
    }
}
