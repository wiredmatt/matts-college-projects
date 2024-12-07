using LogicaNegocio.Enums;

namespace LibreriaWeb.Models.Usuarios
{
    public class ViewModelUsuarioAlta
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
