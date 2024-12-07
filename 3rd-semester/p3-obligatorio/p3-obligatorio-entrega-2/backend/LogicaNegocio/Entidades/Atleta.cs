using LogicaNegocio.Enums;
using LogicaNegocio.ExcepcionesEntidades.Atletas;

namespace LogicaNegocio.Entidades
{
    public class Atleta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }
        public int IdPais { get; set; }
        public Pais Pais { get; set; }
        public List<Disciplina> Disciplinas { get; set; } = [];
        public List<Evento> Eventos { get; set; } = [];
        public List<AtletaEvento> AtletaEventos { get; set; } = [];

        private Atleta() { }

        public Atleta(string nombre, string apellido, Sexo sexo, int idPais)
        {
            Nombre = nombre;
            Apellido = apellido;
            Sexo = sexo;
            IdPais = idPais;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new ExceptionAtleta("El Nombre es requerido");
            }

            if (string.IsNullOrWhiteSpace(Apellido))
            {
                throw new ExceptionAtleta("El Apellido es requerido");
            }

            if (Apellido.Length < 3)
            {
                throw new ExceptionAtleta("El Apellido debe tener al menos 3 caracteres");
            }

            if (Nombre.Length < 3)
            {
                throw new ExceptionAtleta("El Nombre debe tener al menos 3 caracteres");
            }

            if (!Nombre.All(Char.IsLetterOrDigit))
            {
                throw new ExceptionAtleta("El Nombre debe tener solo letras");
            }

            if (!Apellido.All(Char.IsLetterOrDigit))
            {
                throw new ExceptionAtleta("El Apellido debe tener solo letras");
            }
            if (IdPais <= 0)
            {
                throw new ExceptionAtleta("El Pais es requerido");
            }
        }
    }
}