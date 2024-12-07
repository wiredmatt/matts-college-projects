using Compartido.DTOs.Usuarios;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios
{
    public interface IUsuarioListado
    {
        IEnumerable<DTOUsuarioListado> Ejecutar();
    }
}