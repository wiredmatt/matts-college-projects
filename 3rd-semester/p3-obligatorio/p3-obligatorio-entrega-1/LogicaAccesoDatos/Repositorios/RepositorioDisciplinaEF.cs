using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioDisciplinaEF : IRepositorioDisciplina
    {
        public ObligatorioContext DbCtx { get; set; }

        public RepositorioDisciplinaEF(ObligatorioContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        public void Add(Disciplina disciplina)
        {
            Disciplina? disciplinaXNombre = null;
            Disciplina? disciplinaXId = null;

            try { disciplinaXNombre = FindByName(disciplina.Nombre.Valor); }
            catch (ExceptionDisciplina) { }
            if (disciplinaXNombre != null)
                throw new ExceptionDisciplina("Ya existe una Disciplina con ese Nombre");

            try { disciplinaXId = FindById(disciplina.Id); }
            catch (ExceptionDisciplina) { }
            if (disciplinaXId != null)
                throw new ExceptionDisciplina("Ya existe una Disciplina con ese Id");

            DbCtx.Disciplinas.Add(disciplina);
            DbCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            Disciplina disciplina = FindById(id);
            DbCtx.Disciplinas.Remove(disciplina);
            DbCtx.SaveChanges();
        }

        public Disciplina FindByName(string name)
        {
            return DbCtx.Disciplinas
                                .AsEnumerable()
                                .FirstOrDefault(x => x.Nombre.Valor == name) ?? throw new ExceptionDisciplina("No se encontró una Disciplina con ese Nombre");
        }

        public Disciplina FindById(int id)
        {
            return DbCtx.Disciplinas
                            .AsEnumerable()
                            .FirstOrDefault(x => x.Id == id) ?? throw new ExceptionDisciplina("No se encontró una Disciplina con ese Codigo");
        }

        public IEnumerable<Disciplina> FindAllOrdered()
        {
            return DbCtx.Disciplinas.AsEnumerable().OrderBy(x => x.Nombre.Valor);
        }

        public void Update(Disciplina item, int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Disciplina> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
