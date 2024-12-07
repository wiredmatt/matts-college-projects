using LogicaNegocio.Enums;

namespace Compartido.DTOs.Usuarios
{
    public class DTOUsuarioAlta
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario Rol { get; set; }

        public int? IdCreador { get; set; } = null;
    }
}
