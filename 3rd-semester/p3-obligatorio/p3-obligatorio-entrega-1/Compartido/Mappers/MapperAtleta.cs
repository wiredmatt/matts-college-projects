using Compartido.DTOs.Atletas;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Atletas;

namespace Compartido.Mappers
{
    public class MapperAtleta
    {
        public static Atleta DTOAtletaAltaToAtleta(DTOAtletaAlta dtoAtleta)
        {
            if (dtoAtleta == null)
            {
                throw new ExceptionAtleta("Datos incorrectos");
            }

            return new Atleta(dtoAtleta.Nombre, dtoAtleta.Apellido, dtoAtleta.Sexo, dtoAtleta.IdPais);
        }

        public static IEnumerable<DTOAtletaListado> ListAtletaToListDTOAtletaListado(List<Atleta> Atletas)
        {
            IEnumerable<DTOAtletaListado> dtoAtletas = Atletas.Select(a => new DTOAtletaListado()
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                Sexo = a.Sexo,
                Pais = MapperPais.PaisToDTOPaisListado(a.Pais),
                Disciplinas = MapperDisciplina.ListDisciplinaToListDTODisciplinaListado(a?.Disciplinas ?? []),
            });
            return dtoAtletas;
        }
        public static DTOAtletaListado AtletaToDTOAtletaListado(Atleta atleta)
        {
            return new DTOAtletaListado()
            {
                Id = atleta.Id,
                Nombre = atleta.Nombre,
                Apellido = atleta.Apellido,
                Sexo = atleta.Sexo,
                Pais = MapperPais.PaisToDTOPaisListado(atleta.Pais),
                Disciplinas = MapperDisciplina.ListDisciplinaToListDTODisciplinaListado(atleta.Disciplinas),
            };
        }
    }
}