using Compartido.DTOs.Usuarios;
using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Usuarios
{
    public class UsuarioBaja : IUsuarioBaja
    {
        public IRepositorioUsuario RepoUsuario { get; set; }

        public UsuarioBaja(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public void Ejecutar(DTOUsuarioBaja dtoUsuarioBaja)
        {
            RepoUsuario.Delete(dtoUsuarioBaja.Id);
        }
    }
}
