using Compartido.DTOs.Usuarios;
using Compartido.Mappers;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioAlta : IUsuarioAlta
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioAlta(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public void Ejecutar(DTOUsuarioAlta dtoUsuario)
        {
            RepoUsuario.Add(MapperUsuario.DTOUsuarioAltaToUsuario(dtoUsuario));
        }
    }
}
