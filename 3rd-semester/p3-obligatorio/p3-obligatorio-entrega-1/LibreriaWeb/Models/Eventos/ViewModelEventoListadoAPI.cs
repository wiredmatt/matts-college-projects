using LibreriaWeb.Models.Disciplinas;

namespace LibreriaWeb.Models.Eventos
{
    public class ViewModelEventoListadoAPI
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ViewModelDisciplinaListadoAPI Disciplina { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}
