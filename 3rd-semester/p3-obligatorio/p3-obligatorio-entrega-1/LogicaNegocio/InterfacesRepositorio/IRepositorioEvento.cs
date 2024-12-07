using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEvento : IRepositorio<Evento>
    {
        public IEnumerable<Evento> FindByDate(DateOnly date);
        public void SetAtletaEventoPuntaje(int idEvento, int idAtleta, double puntaje);
        public IEnumerable<Evento> FindAllByIdAtleta(int idAtleta);
        public AtletaEvento FindParticipacionByEventoIdAndAtletaId(int idEvento, int idAtleta);
    }
}
