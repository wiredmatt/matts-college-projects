namespace Compartido.DTOs.Eventos
{
    public class DTOEventoAlta
    {
        public string Nombre { get; set; }
        public int IdDisciplina { get; set; }
        public List<int> IdsAtletas { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}
