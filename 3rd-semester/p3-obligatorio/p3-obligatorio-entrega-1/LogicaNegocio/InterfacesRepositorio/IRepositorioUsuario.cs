using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        public IEnumerable<Usuario> FindAllOrdered();
        public Usuario FindByCredentials(string email, string password);
        public Usuario FindByEmail(string email);
    }
}
