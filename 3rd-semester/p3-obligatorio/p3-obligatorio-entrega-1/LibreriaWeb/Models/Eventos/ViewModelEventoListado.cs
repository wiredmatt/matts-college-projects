using System.ComponentModel;
using LibreriaWeb.Models.Atletas;
using LibreriaWeb.Models.Disciplinas;

namespace LibreriaWeb.Models.Eventos
{
    public class ViewModelEventoListado
    {
        public int Id { get; set; }
        [DisplayName("Nombre de la Prueba")]
        public string Nombre { get; set; }
        public ViewModelDisciplinaListado Disciplina { get; set; }
        public IEnumerable<ViewModelAtletaListado> Atletas { get; set; }
        [DisplayName("Fecha de Inicio")]
        public DateOnly FechaInicio { get; set; }
        [DisplayName("Fecha de Fin")]
        public DateOnly FechaFin { get; set; }
    }
}
