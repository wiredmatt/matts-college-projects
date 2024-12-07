using Compartido.DTOs.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;

namespace Compartido.Mappers
{
    public class MapperUsuario
    {
        public static Usuario DTOUsuarioAltaToUsuario(DTOUsuarioAlta dtoUsuario)
        {
            if (dtoUsuario == null)
            {
                throw new ExceptionUsuario("Datos incorrectos");
            }

            return new Usuario(dtoUsuario.Email, dtoUsuario.Contrasena, dtoUsuario.Rol, dtoUsuario.IdCreador);
        }

        public static Usuario DTOUsuarioEditarToUsuario(DTOUsuarioEditar dtoUsuario)
        {
            if (dtoUsuario == null)
            {
                throw new ExceptionUsuario("Datos incorrectos");
            }

            return new Usuario(dtoUsuario.Email, dtoUsuario.Contrasena, dtoUsuario.Rol);
        }

        public static IEnumerable<DTOUsuarioListado> ListUsuarioToListDTOUsuarioListado(List<Usuario> Usuarios)
        {
            IEnumerable<DTOUsuarioListado> dtoUsuarios = Usuarios.Select(p => new DTOUsuarioListado()
            {
                Id = p.Id,
                Email = p.Email.Valor,
                Rol = p.Rol
            });
            return dtoUsuarios;
        }

        public static DTOUsuarioListado UsuarioToDTOUsuarioListado(Usuario usuario)
        {
            return new DTOUsuarioListado()
            {
                Id = usuario.Id,
                Email = usuario.Email.Valor,
                Rol = usuario.Rol
            };
        }

        public static DTOUsuarioLogeado UsuarioToUsuarioLogeadoDTO(Usuario usuario)
        {
            return new DTOUsuarioLogeado
            {
                Id = usuario.Id,
                Email = usuario.Email.Valor,
                Rol = usuario.Rol
            };
        }
    }
}
