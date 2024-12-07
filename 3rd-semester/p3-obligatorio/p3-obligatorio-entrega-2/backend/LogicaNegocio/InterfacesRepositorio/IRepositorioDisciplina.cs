using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioDisciplina : IRepositorioAuditable<Disciplina>
    {
        IEnumerable<Disciplina> FindAllOrdered();

        Disciplina FindByName(string name);
    }
}
