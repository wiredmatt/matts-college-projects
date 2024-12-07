using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAtleta : IRepositorio<Atleta>
    {
        public IEnumerable<Atleta> FindAllOrdered();
    }
}
