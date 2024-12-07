namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T>
    {
        T Add(T item);
        void Delete(int id);
        T Update(T item, int id);
        T FindById(int id);
        IEnumerable<T> FindAll();
    }
}
