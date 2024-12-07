namespace LogicaNegocio.ExcepcionesEntidades.Paises
{
    public class ExceptionPais : Exception
    {
        public ExceptionPais() { }
        public ExceptionPais(string message) : base(message) { }

        public ExceptionPais(string message, Exception innerException) : base(message, innerException) { }
    }
}
