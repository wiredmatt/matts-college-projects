using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record NombrePais : IComparable<NombrePais>, IEquatable<NombrePais>
    {
        public string Valor { get; init; }

        public NombrePais(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (Valor.Trim().Length < 3 || Valor.Trim().Length > 50)
            {
                throw new ExceptionDisciplina
                    ("El Nombre del Pais tiene que tener entre 3 y 50 caracteres");
            }
        }

        public int CompareTo(NombrePais? other)
        {
            return Valor.CompareTo(other?.Valor);
        }

        public virtual bool Equals(NombrePais? other)
        {
            return Valor.Equals(other?.Valor);
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
