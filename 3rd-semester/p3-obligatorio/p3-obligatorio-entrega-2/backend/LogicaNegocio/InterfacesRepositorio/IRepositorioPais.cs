using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPais : IRepositorio<Pais>
    {
        public IEnumerable<Pais> FindAllOrdered();
        public Pais FindByName(string name);
    }
}
