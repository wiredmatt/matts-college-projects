using Compartido.DTOs.Usuarios;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios
{
    public interface IUsuarioAlta
    {
        void Ejecutar(DTOUsuarioAlta dtoUsuario);
    }
}
