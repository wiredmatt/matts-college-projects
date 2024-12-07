using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioBuscar : IUsuarioBuscar
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioBuscar(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public DTOUsuarioListado Ejecutar(int id)
        {
            Usuario usuario = RepoUsuario.FindById(id);
            return MapperUsuario.UsuarioToDTOUsuarioListado(usuario);
        }
    }
}
