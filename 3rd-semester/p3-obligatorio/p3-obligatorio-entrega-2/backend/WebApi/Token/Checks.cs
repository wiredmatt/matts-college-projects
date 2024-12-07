using LogicaNegocio.Enums;

namespace WebApi.Token
{
    public static class Checks
    {
        static public void CheckRolUsuario(RolUsuario rolRequerido, string rol)
        {
            var parseOk = Enum.TryParse(rol, out RolUsuario _);
            if (!parseOk) throw new Exception("No cuenta con el rol requerido: " + rolRequerido);
            if (rolRequerido.ToString() != rol) throw new Exception("No cuenta con el rol requerido: " + rolRequerido);
        }
    }
}