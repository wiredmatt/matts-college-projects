using WebApp.Enums;

namespace WebApp
{
    public static class Checks
    {
        static public bool CheckRolUsuario(RolUsuario rolRequerido, string? rol)
        {
            if (rol == null) return false;

            var parseOk = Enum.TryParse(rol, out RolUsuario rolUsuario);
            if (!parseOk) return false;

            return rolRequerido == rolUsuario;
        }
    }
}