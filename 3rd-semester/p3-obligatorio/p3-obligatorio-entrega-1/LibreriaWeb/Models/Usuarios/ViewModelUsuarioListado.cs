using LogicaNegocio.Enums;

namespace LibreriaWeb.Models.Usuarios
{
    public class ViewModelUsuarioListado
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
