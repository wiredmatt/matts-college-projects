using Compartido.DTOs.Usuarios;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios
{
    public interface IUsuarioLogin
    {
        DTOUsuarioLogeado Ejecutar(DTOUsuarioLogin dtoUsuarioLogin);
    }
}
