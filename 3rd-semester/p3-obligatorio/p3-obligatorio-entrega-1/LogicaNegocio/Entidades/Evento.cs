using LogicaNegocio.ValueObject;
using LogicaNegocio.ExcepcionesEntidades.Eventos;

namespace LogicaNegocio.Entidades
{
    public class Evento
    {
        public int Id { get; set; }
        public NombreEvento Nombre { get; set; }
        public int IdDisciplina { get; set; }
        public Disciplina Disciplina { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public List<Atleta> Atletas { get; set; } = [];
        public List<AtletaEvento> AtletaEventos { get; set; } = [];

        public Evento() { }

        public Evento(string nombre, DateOnly fechaInicio, DateOnly fechaFin, int idDisciplina)
        {
            Nombre = new NombreEvento(nombre);

            IdDisciplina = idDisciplina;

            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

            // El caso de uso llamara .Validar para este caso, luego de settear a los Atletas.
        }

        public Evento(string nombre, Disciplina disciplina, List<Atleta> atletas, DateOnly fechaInicio, DateOnly fechaFin)
        {
            Nombre = new NombreEvento(nombre);

            IdDisciplina = disciplina.Id;
            Disciplina = disciplina;

            Atletas = atletas;

            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

            Validar();
        }

        public void Validar()
        {
            if (this.IdDisciplina <= 0)
            {
                throw new ExceptionEvento("La Disciplina es requerida");
            }

            if (this.Atletas.Count < 3)
            {
                throw new ExceptionEvento("El Evento debe tener al menos 3 Atletas asignados");
            }

            if (this.FechaInicio > this.FechaFin)
            {
                throw new ExceptionEvento("La Fecha de Inicio del Eevento no puede ser mayor a la Fecha de Fin");
            }

            if (this.FechaInicio < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ExceptionEvento("La Fecha de Inicio del Evento no puede ser menor a la Fecha actual");
            }
        }
    }
}