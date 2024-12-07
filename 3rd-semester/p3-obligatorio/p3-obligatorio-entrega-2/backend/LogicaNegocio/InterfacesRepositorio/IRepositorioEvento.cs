using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEvento : IRepositorio<Evento>
    {
        public IEnumerable<Evento> FindByDate(DateOnly date);
        public IEnumerable<Evento> FindByDateRange(DateOnly? inicio, DateOnly? fin);
        public IEnumerable<Evento> FindByIdDisciplina(int idDisciplina);
        public IEnumerable<Evento> FindByNameLike(string name);
        public IEnumerable<Evento> FindByPuntajeRange(double? minimo, double? maximo);

        public void SetAtletaEventoPuntaje(int idEvento, int idAtleta, double puntaje);
        public IEnumerable<Evento> FindAllByIdAtleta(int idAtleta);
        public AtletaEvento FindParticipacionByEventoIdAndAtletaId(int idEvento, int idAtleta);
    }
}
