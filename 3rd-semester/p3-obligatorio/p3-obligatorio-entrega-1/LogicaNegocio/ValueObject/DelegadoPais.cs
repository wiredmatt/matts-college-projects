using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.ExcepcionesEntidades.Paises;

namespace LogicaNegocio.ValueObject
{
    [ComplexType]
    public record DelegadoPais
    {
        public string Nombre { get; init; }
        public string Telefono { get; init; }

        public DelegadoPais(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            Validar();
        }

        private void Validar()
        {
            if (Nombre.Trim().Length < 3 || Nombre.Trim().Length > 50)
            {
                throw new ExceptionPais
                    ("El Nombre del Delegado debe tener entre 3 y 50 caracteres");
            }

            // see https://www.twilio.com/docs/glossary/what-e164
            // see https://worldpopulationreview.com/country-rankings/phone-number-length-by-country
            if (Telefono.Trim().Length < 4 || Telefono.Trim().Length > 20)
            {
                throw new ExceptionPais
                    ("El Telefono del Delegado tiene que tener entre 4 y 20 caracteres.");
            }

            if (!Telefono.StartsWith('+'))
            {
                throw new ExceptionPais
                    ("El Telefono del Delegado tiene que empezar con '+'. Asegurese de incluir la caracteristica del Pais.");
            }

            // todo: regex?
        }
    }
}
