using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades;
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

        public Disciplina Add(Disciplina disciplina, string emailUsuario)
        {
            Disciplina? disciplinaXNombre = null;
            Disciplina? disciplinaXId = null;

            try { disciplinaXNombre = FindByName(disciplina.Nombre.Valor); }
            catch (ExceptionDisciplina) { }
            if (disciplinaXNombre != null)
                throw new ConflictException("Ya existe una Disciplina con ese Nombre");

            try { disciplinaXId = FindById(disciplina.Id); }
            catch (ExceptionDisciplina) { }
            if (disciplinaXId != null)
                throw new ConflictException("Ya existe una Disciplina con ese Id");

            DbCtx.Disciplinas.Add(disciplina);
            DbCtx.SaveChanges(emailUsuario);

            return disciplina;
        }

        public void Delete(int id, string emailUsuario)
        {
            Disciplina disciplina = FindById(id);
            DbCtx.Disciplinas.Remove(disciplina);
            DbCtx.SaveChanges(emailUsuario);
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

        public Disciplina Update(Disciplina item, int id, string emailUsuario)
        {
            Disciplina disciplina = FindById(id);

            disciplina.Nombre = item.Nombre;
            disciplina.Ano = item.Ano;

            disciplina.Validar();

            DbCtx.SaveChanges(emailUsuario);

            return disciplina;
        }


        public IEnumerable<Disciplina> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
