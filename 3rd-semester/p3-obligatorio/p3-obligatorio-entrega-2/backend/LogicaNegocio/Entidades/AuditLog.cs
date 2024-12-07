namespace LogicaNegocio.Entidades
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string TipoEntidad { get; set; }
        public int IdEntidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Operacion { get; set; }
        public string EmailUsuario { get; set; }
    }
}