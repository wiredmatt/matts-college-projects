using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Paises;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioPaisEF : IRepositorioPais
    {
        public ObligatorioContext DbCtx { get; set; }

        public RepositorioPaisEF(ObligatorioContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        public Pais Add(Pais item)
        {
            Pais? paisXNombre = null;
            try { paisXNombre = FindByName(item.Nombre.Valor); }
            catch (ExceptionPais) { }

            if (paisXNombre != null)
                throw new ExceptionPais("Ya existe un Pais con ese Nombre");

            DbCtx.Paises.Add(item);
            DbCtx.SaveChanges();

            return item;
        }

        public IEnumerable<Pais> FindAllOrdered()
        {
            return DbCtx.Paises.AsEnumerable().OrderBy(x => x.Id);
        }

        public Pais FindById(int id)
        {
            return DbCtx.Paises
                            .AsEnumerable()
                            .FirstOrDefault(u => u.Id == id) ?? throw new ExceptionPais("No existe un Pais con ese Id");
        }

        public Pais FindByName(string name)
        {

            return DbCtx.Paises
                            .AsEnumerable()
                            .FirstOrDefault(u => u.Nombre.Valor == name) ?? throw new ExceptionPais("No existe un Pais con ese Nombre");
        }

        public IEnumerable<Pais> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Pais Update(Pais item, int id)
        {
            throw new NotImplementedException();
        }
    }
}