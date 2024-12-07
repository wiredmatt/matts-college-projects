using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioLogin : IUsuarioLogin
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioLogin(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public DTOUsuarioLogeado Ejecutar(DTOUsuarioLogin dtoUsuarioLogin)
        {
            Usuario usuario = RepoUsuario.FindByCredentials(
                dtoUsuarioLogin.Email, dtoUsuarioLogin.Contrasena
            );

            DTOUsuarioLogeado dtoUsuarioLogeado = MapperUsuario.UsuarioToUsuarioLogeadoDTO(usuario);

            return dtoUsuarioLogeado;
        }
    }
}
