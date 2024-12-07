using WebApp.Enums;

namespace WebApp.Models.Usuarios
{
    public class ViewModelUsuarioLogeado
    {
        required public int Id { get; set; }
        required public string Email { get; set; }
        required public RolUsuario Rol { get; set; }
    }

    public class ViewModelUsuarioLogeadoConToken : ViewModelUsuarioLogeado
    {
        required public string Token { get; set; }
    }
}