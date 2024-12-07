using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Atletas;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAtletaEF : IRepositorioAtleta
    {
        public ObligatorioContext DbCtx { get; set; }

        public RepositorioAtletaEF(ObligatorioContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        public void Add(Atleta item)
        {
            DbCtx.Atletas.Add(item);
            DbCtx.SaveChanges();
        }

        public IEnumerable<Atleta> FindAllOrdered()
        {
            // ordenado por nombre del país y luego por apellido y nombre del atleta en forma alfabética, de la A a la Z
            return DbCtx.Atletas
                            .Include(a => a.Pais)
                            .AsEnumerable()
                            .OrderBy(a => a.Pais.Nombre)
                            .ThenBy(a => a.Apellido)
                            .ThenBy(a => a.Nombre);
        }

        public Atleta FindById(int id)
        {
            return DbCtx.Atletas
                            .Include(a => a.Pais)
                            .Include(a => a.Disciplinas)
                            .Include(a => a.Eventos)
                            .AsEnumerable()
                            .FirstOrDefault(u => u.Id == id) ?? throw new ExceptionAtleta("No existe un Atleta con ese Id");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Atleta item, int id)
        {
            Atleta atletaXId = FindById(id);

            // set the new Disciplinas
            atletaXId.Disciplinas = item.Disciplinas;

            DbCtx.SaveChanges();
        }

        public IEnumerable<Atleta> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}