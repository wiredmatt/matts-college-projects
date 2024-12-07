using LogicaNegocio.Enums;

namespace LibreriaWeb.Models.Usuarios
{
    public class ViewModelUsuarioEditar
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
