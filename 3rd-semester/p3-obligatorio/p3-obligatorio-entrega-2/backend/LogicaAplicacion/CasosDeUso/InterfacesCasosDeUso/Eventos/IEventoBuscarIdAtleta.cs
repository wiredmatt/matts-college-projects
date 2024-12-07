using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoBuscarIdAtleta
    {
        IEnumerable<DTOEventoListado> Ejecutar(int idAtleta);
    }
}
