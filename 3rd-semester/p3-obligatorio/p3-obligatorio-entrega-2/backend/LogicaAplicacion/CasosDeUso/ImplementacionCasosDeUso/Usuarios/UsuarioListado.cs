using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioListado : IUsuarioListado
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioListado(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public IEnumerable<DTOUsuarioListado> Ejecutar()
        {
            List<Usuario> Usuarios = RepoUsuario.FindAllOrdered().ToList();
            return MapperUsuario.ListUsuarioToListDTOUsuarioListado(Usuarios);
        }
    }
}