using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoBuscarId
    {
        DTOEventoListado Ejecutar(int id);
    }
}
