namespace LogicaNegocio.ExcepcionesEntidades.Usuarios
{
    public class ExceptionUsuario : Exception
    {
        public ExceptionUsuario() { }
        public ExceptionUsuario(string message) : base(message) { }

        public ExceptionUsuario(string message, Exception innerException) : base(message, innerException) { }
    }
}
