using LogicaNegocio.ValueObject;
using LogicaNegocio.ExcepcionesEntidades.Paises;

namespace LogicaNegocio.Entidades
{

    public class Pais : IEquatable<Pais>
    {
        public int Id { get; set; }
        public NombrePais Nombre { get; set; }
        public int CantidadHabitantes { get; set; }
        public DelegadoPais Delegado { get; set; }

        private Pais() { }

        public Pais(int id) { Id = id; }

        public Pais(string nombrePais, int cantidadHabitantes, string nombreDelegado, string telefonoDelegado)
        {
            Nombre = new NombrePais(nombrePais);
            CantidadHabitantes = cantidadHabitantes;
            Delegado = new DelegadoPais(nombreDelegado, telefonoDelegado);
            Validar();
        }

        public void Validar()
        {
            if (CantidadHabitantes <= 0)
            {
                throw new ExceptionPais("La cantidad de habitantes debe ser mayor a 0");
            }
        }

        public bool Equals(Pais? other)
        {
            return Id.Equals(other?.Id) || Nombre.Equals(other?.Nombre);
        }
    }
}
