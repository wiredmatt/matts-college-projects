using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioDisciplina : IRepositorio<Disciplina>
    {
        IEnumerable<Disciplina> FindAllOrdered();

        Disciplina FindByName(string name);
    }
}
