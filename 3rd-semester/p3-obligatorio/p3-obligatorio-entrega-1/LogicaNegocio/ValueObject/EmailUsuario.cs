using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record EmailUsuario : IEquatable<EmailUsuario>
    {
        public string Valor { get; init; }

        public EmailUsuario(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new ExceptionUsuario("El email es requerido");
            }

            // a@b.cd
            if (Valor.Length < 6)
            {
                throw new ExceptionUsuario("El email es muy corto");
            }

            if (Valor.Length > 320)
            {
                throw new ExceptionUsuario("El email es muy largo");
            }

            if (!Valor.Contains('@'))
            {
                throw new ExceptionUsuario("El email no es valido");
            }
        }

        public virtual bool Equals(EmailUsuario? other)
        {
            return Valor.Equals(other?.Valor);
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
