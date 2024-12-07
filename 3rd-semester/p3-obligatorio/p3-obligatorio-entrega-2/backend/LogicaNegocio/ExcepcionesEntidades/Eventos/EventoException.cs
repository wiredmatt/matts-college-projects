namespace LogicaNegocio.ExcepcionesEntidades.Eventos
{
    public class ExceptionEvento : Exception
    {
        public ExceptionEvento() { }
        public ExceptionEvento(string message) : base(message) { }

        public ExceptionEvento(string message, Exception innerException) : base(message, innerException) { }
    }
}
