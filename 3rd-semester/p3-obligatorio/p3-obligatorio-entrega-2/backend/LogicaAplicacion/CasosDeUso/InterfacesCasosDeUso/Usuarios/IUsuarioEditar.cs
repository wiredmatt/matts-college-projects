using Compartido.DTOs.Usuarios;

namespace LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios
{
    public interface IUsuarioEditar
    {
        void Ejecutar(DTOUsuarioEditar dtoUsuario, int id);
    }
}
