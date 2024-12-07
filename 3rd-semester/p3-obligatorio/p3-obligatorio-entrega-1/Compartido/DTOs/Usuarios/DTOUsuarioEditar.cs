using LogicaNegocio.Enums;

namespace Compartido.DTOs.Usuarios
{
    public class DTOUsuarioEditar
    {
        public string Email { get; set; }
        public string? Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
