namespace LogicaNegocio.ExcepcionesEntidades.Atletas
{
    public class ExceptionAtleta : Exception
    {
        public ExceptionAtleta() { }
        public ExceptionAtleta(string message) : base(message) { }

        public ExceptionAtleta(string message, Exception innerException) : base(message, innerException) { }
    }
}
