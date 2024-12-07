using LogicaNegocio.Enums;
using LogicaNegocio.ValueObject;

namespace LogicaNegocio.Entidades
{


    public class Usuario : IEquatable<Usuario>
    {
        public int Id { get; set; }
        public EmailUsuario Email { get; set; }
        public ContrasenaUsuario Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
        public DateTime FechaAlta { get; set; } // setteada desde el caso de uso
        public int? IdCreador { get; set; } = null;
        public Usuario? Creador { get; set; } = null;

        public override string ToString()
        {
            return $"Id: {Id}, Email: {Email}, Rol: {Rol}, FechaAlta: {FechaAlta}, IdCreador: {IdCreador}";
        }

        private Usuario() { }

        public Usuario(string email, string contrasena, RolUsuario rol, int? idCreador)
        {
            Email = new EmailUsuario(email);
            Contrasena = new ContrasenaUsuario(contrasena);
            Rol = rol;
            IdCreador = idCreador;
            Validar();
        }

        // constructor para Editar usuario, la contraseña es opcional 
        // y no se modifica el IdCreador
        public Usuario(string email, string? contrasena, RolUsuario rol)
        {
            Email = new EmailUsuario(email);

            if (contrasena != null)
            {
                Contrasena = new ContrasenaUsuario(contrasena);
            }

            Rol = rol;
            Validar();
        }

        public void Validar() { }

        public bool Equals(Usuario? other)
        {
            return Email.Equals(other?.Email) || Id.Equals(other?.Id);
        }
    }
}
