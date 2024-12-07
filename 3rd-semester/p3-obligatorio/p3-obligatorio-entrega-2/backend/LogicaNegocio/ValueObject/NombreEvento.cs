using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Disciplinas;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record NombreEvento : IComparable<NombreEvento>, IEquatable<NombreEvento>
    {
        public string Valor { get; init; }

        public NombreEvento(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (Valor.Trim().Length < 3 || Valor.Trim().Length > 100)
            {
                throw new ExceptionDisciplina
                    ("El Nombre de la prueba del Evento debe tener entre 3 y 100 caracteres");
            }
        }

        public int CompareTo(NombreEvento? other)
        {
            return Valor.CompareTo(other?.Valor);
        }

        public virtual bool Equals(NombreEvento? other)
        {
            return Valor.Equals(other?.Valor);
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
