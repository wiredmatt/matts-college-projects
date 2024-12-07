using LogicaNegocio.ExcepcionesEntidades.Disciplinas;
using LogicaNegocio.ValueObject;

namespace LogicaNegocio.Entidades
{
    public class Disciplina : IAuditable
    {
        public int Id { get; set; }

        public NombreDisciplina Nombre { get; set; }

        public int Ano { get; set; }

        public List<Atleta> Atletas { get; } = [];

        private Disciplina() { }

        public Disciplina(string nombre, int id, int ano)
        {
            Nombre = new NombreDisciplina(nombre);
            Id = id;
            Ano = ano;
            Validar();
        }

        public Disciplina(string nombre, int ano)
        {
            Nombre = new NombreDisciplina(nombre);
            Ano = ano;
        }

        public void Validar()
        {
            if (Id <= 0)
            {
                throw new ExceptionDisciplina("El Codigo debe ser mayor a 0");
            }
            if (Ano < 1896) // Primera edicion de los juegos olimpicos
            {
                throw new ExceptionDisciplina("El año debe ser mayor o igual a 1896");
            }
        }
    }
}
