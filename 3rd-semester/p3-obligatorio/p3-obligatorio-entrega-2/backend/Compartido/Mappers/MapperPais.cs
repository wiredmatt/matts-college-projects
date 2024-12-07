using Compartido.DTOs.Paises;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Paises;

namespace Compartido.Mappers
{
    public class MapperPais
    {
        public static Pais DTOPaisAltaToPais(DTOPaisAlta dtoPais)
        {
            if (dtoPais == null)
            {
                throw new ExceptionPais("Datos incorrectos");
            }

            return new Pais(dtoPais.Nombre, dtoPais.CantidadHabitantes, dtoPais.NombreDelegado, dtoPais.TelefonoDelegado);
        }

        public static IEnumerable<DTOPaisListado> ListPaisToListDTOPaisListado(List<Pais> paises)
        {
            IEnumerable<DTOPaisListado> dtoPaisListado = paises.Select(p => new DTOPaisListado()
            {
                Id = p.Id,
                Nombre = p.Nombre.Valor,
                CantidadHabitantes = p.CantidadHabitantes,
                NombreDelegado = p.Delegado.Nombre,
                TelefonoDelegado = p.Delegado.Telefono
            });
            return dtoPaisListado;
        }

        public static DTOPaisListado PaisToDTOPaisListado(Pais p)
        {
            return new DTOPaisListado()
            {
                Id = p.Id,
                Nombre = p.Nombre.Valor,
                CantidadHabitantes = p.CantidadHabitantes,
                NombreDelegado = p.Delegado.Nombre,
                TelefonoDelegado = p.Delegado.Telefono
            };
        }
    }
}