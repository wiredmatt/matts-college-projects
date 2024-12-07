using Compartido.DTOs.Eventos;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Eventos
{
    public interface IAtletaEventoBuscar
    {
        DTOAtletaEventoListado Ejecutar(int idEvento, int idAtleta);
    }
}
