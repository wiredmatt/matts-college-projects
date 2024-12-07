using LogicaNegocio.Enums;

namespace Compartido.DTOs.Usuarios
{
    public class DTOUsuarioLogeado
    {
        required public int Id { get; set; }
        required public string Email { get; set; }
        required public RolUsuario Rol { get; set; }
    }
}
