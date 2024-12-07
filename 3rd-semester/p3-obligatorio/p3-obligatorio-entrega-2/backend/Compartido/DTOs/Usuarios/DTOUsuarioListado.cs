using LogicaNegocio.Enums;

namespace Compartido.DTOs.Usuarios
{
    public class DTOUsuarioListado
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
