using Compartido.DTOs.Atletas;
using Compartido.DTOs.Disciplinas;

namespace Compartido.DTOs.Eventos
{
    public class DTOEventoListado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DTODisciplinaListado Disciplina { get; set; }
        public IEnumerable<DTOAtletaListado> Atletas { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}
