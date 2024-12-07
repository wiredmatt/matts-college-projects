using WebApp.Models.Atletas;
using WebApp.Models.Disciplinas;

namespace WebApp.Models.Eventos
{
    public class ViewModelEventoListado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ViewModelDisciplinaListado Disciplina { get; set; }
        public IEnumerable<ViewModelAtletaListado> Atletas { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}