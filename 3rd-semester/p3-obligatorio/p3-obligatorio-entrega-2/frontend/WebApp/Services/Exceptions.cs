namespace WebApp.Services
{
    public class CustomHTTPException : Exception
    {
        public string Error { get; set; } = "";

        public CustomHTTPException() { }

        public CustomHTTPException(string error) : base(error)
        {
            Error = error;
        }

        public CustomHTTPException(string error, Exception innerException) : base(error, innerException)
        {
            Error = error;
        }
    }
}
