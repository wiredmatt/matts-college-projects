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

        public Atleta Add(Atleta item)
        {
            DbCtx.Atletas.Add(item);
            DbCtx.SaveChanges();

            return item;
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

        public IEnumerable<Atleta> FindByIdDisciplina(int idDisciplina)
        {
            return DbCtx.Atletas
                            .Include(a => a.Disciplinas)
                            .Include(a => a.Pais)
                            .Where(a => a.Disciplinas.Any(d => d.Id == idDisciplina))
                            .OrderBy(a => a.Apellido)
                            .ThenBy(a => a.Nombre)
                            .AsEnumerable();
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

        public Atleta Update(Atleta item, int id)
        {
            Atleta atletaXId = FindById(id);

            // set the new Disciplinas
            atletaXId.Disciplinas = item.Disciplinas;

            DbCtx.SaveChanges();

            return atletaXId;
        }

        public IEnumerable<Atleta> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}