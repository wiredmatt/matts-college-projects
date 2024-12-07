namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAuditable<T>
    {
        T Add(T item, string emailUsuario);
        void Delete(int id, string emailUsuario);
        T Update(T item, int id, string emailUsuario);
        T FindById(int id);
        IEnumerable<T> FindAll();
    }
}
