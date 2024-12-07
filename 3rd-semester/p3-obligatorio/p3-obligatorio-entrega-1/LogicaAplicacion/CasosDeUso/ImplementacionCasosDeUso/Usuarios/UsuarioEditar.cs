using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioEditar : IUsuarioEditar
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioEditar(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public void Ejecutar(DTOUsuarioEditar dtoUsuario, int id)
        {
            RepoUsuario.Update(MapperUsuario.DTOUsuarioEditarToUsuario(dtoUsuario), id);
        }
    }
}
