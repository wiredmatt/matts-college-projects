using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record NombreDisciplina : IComparable<NombreDisciplina>, IEquatable<NombreDisciplina>
    {
        public string Valor { get; init; }

        public NombreDisciplina(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (Valor.Trim().Length < 10 || Valor.Trim().Length > 50)
            {
                throw new ExceptionDisciplina
                    ("El Nombre debe tener entre 10 y 50 caracteres");
            }
        }

        public int CompareTo(NombreDisciplina? other)
        {
            return Valor.CompareTo(other?.Valor);
        }

        public virtual bool Equals(NombreDisciplina? other)
        {
            return Valor.Equals(other?.Valor);
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
