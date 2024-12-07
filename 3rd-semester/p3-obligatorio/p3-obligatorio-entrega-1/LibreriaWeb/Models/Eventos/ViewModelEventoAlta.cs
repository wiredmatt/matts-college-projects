using System.ComponentModel;

namespace LibreriaWeb.Models.Eventos
{
    public class ViewModelEventoAlta
    {
        [DisplayName("Nombre de la Prueba")]
        public string Nombre { get; set; }

        [DisplayName("Disciplina")]
        public int IdDisciplina { get; set; }

        [DisplayName("Atletas")]
        public List<int> IdsAtletas { get; set; }

        [DisplayName("Fecha de Inicio")]
        public DateOnly FechaInicio { get; set; }

        [DisplayName("Fecha de Fin")]
        public DateOnly FechaFin { get; set; }
    }
}
