using Compartido.DTOs.Usuarios;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios
{
    public interface IUsuarioBuscar
    {
        DTOUsuarioListado Ejecutar(int id);
    }
}
