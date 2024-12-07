using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IEventoBuscarFiltros
    {
        IEnumerable<DTOEventoListado> Ejecutar(int? idDisciplina,
                                               DateOnly? fechaInicio,
                                               DateOnly? fechaFin,
                                               string? nombreEvento,
                                               double? puntajeMinimo,
                                               double? puntajeMaximo);
    }
}
