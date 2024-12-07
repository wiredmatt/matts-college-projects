using Compartido.DTOs.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Eventos;

namespace Compartido.Mappers
{
    public class MapperEvento
    {
        public static Evento DTOEventoAltaToEvento(DTOEventoAlta dtoEvento)
        {
            if (dtoEvento == null)
            {
                throw new ExceptionEvento("Datos incorrectos");
            }

            return new Evento(dtoEvento.Nombre, dtoEvento.FechaInicio, dtoEvento.FechaFin, dtoEvento.IdDisciplina);
        }

        public static DTOEventoListado EventoToDTOEventoListado(Evento evento)
        {
            if (evento == null)
            {
                throw new ExceptionEvento("Datos incorrectos");
            }

            return new DTOEventoListado()
            {
                Id = evento.Id,
                Nombre = evento.Nombre.Valor,
                FechaInicio = evento.FechaInicio,
                FechaFin = evento.FechaFin,
                Disciplina = MapperDisciplina.DisciplinaToDTODisciplinaListado(evento.Disciplina),
                Atletas = MapperAtleta.ListAtletaToListDTOAtletaListado(evento.Atletas),
            };
        }

        public static IEnumerable<DTOEventoListado> ListEventoToListDTOEventoListado(IEnumerable<Evento> eventos)
        {
            return eventos.Select(EventoToDTOEventoListado);
        }

        public static DTOAtletaEventoListado AtletaEventoToDTOAtletaEventoListado(AtletaEvento atletaEvento)
        {
            if (atletaEvento == null)
            {
                throw new ExceptionEvento("Datos incorrectos");
            }

            return new DTOAtletaEventoListado()
            {
                IdAtleta = atletaEvento.IdAtleta,
                IdEvento = atletaEvento.IdEvento,
                Puntaje = atletaEvento.Puntaje,
            };
        }
    }
}