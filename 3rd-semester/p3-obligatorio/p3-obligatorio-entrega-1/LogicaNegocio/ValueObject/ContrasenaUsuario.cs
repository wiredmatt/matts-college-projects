using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record ContrasenaUsuario : IEquatable<ContrasenaUsuario>
    {
        public string Valor { get; init; }

        public ContrasenaUsuario(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new ExceptionUsuario("La Contraseña es requerida");
            }

            if (Valor.Length < 6)
            {
                throw new ExceptionUsuario("La Contraseña es muy corta");
            }

            // al menos una letra minuscula
            if (!Valor.Any(char.IsLower))
            {
                throw new ExceptionUsuario("La Contraseña debe tener al menos una letra minuscula");
            }

            // al menos una letra mayuscula
            if (!Valor.Any(char.IsUpper))
            {
                throw new ExceptionUsuario("La Contraseña debe tener al menos una letra mayuscula");
            }

            // al menos un digito
            if (!Valor.Any(char.IsDigit))
            {
                throw new ExceptionUsuario("La Contraseña debe tener al menos un digito");
            }

            // al menos un caracter de puntuacion (. , ; !)
            if (!Valor.Any(c => c == '.' || c == ',' || c == ';' || c == '!'))
            {
                throw new ExceptionUsuario("La Contraseña debe tener al menos un caracter de puntuacion (. , ; !)");
            }
        }

        public virtual bool Equals(ContrasenaUsuario? other)
        {
            return Valor.Equals(other?.Valor);
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
