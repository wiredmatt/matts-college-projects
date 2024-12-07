using LogicaAplicacion.CasosDeUso.InterfacesCasosDeUso.Disciplinas;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosDeUso.ImplementacionCasosDeUso.Disciplinas
{
    public class DisciplinaBaja : IDisciplinaBaja
    {
        public IRepositorioDisciplina RepoDisciplina { get; set; }

        public DisciplinaBaja(IRepositorioDisciplina repoDisciplina)
        {
            RepoDisciplina = repoDisciplina;
        }

        public void Ejecutar(int id, string emailUsuario)
        {
            RepoDisciplina.Delete(id, emailUsuario);
        }
    }
}
