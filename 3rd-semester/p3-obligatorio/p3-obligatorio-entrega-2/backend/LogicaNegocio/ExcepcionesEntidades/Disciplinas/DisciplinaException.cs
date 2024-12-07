namespace LogicaNegocio.ExcepcionesEntidades.Disciplinas
{
    public class ExceptionDisciplina : Exception
    {
        public ExceptionDisciplina() { }
        public ExceptionDisciplina(string message) : base(message) { }

        public ExceptionDisciplina(string message, Exception innerException) : base(message, innerException) { }
    }
}
