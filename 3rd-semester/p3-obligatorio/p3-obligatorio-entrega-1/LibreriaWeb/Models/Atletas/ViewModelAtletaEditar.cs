using LibreriaWeb.Models.Disciplinas;
using LibreriaWeb.Models.Paises;
using LogicaNegocio.Enums;

namespace LibreriaWeb.Models.Atletas
{
    public class ViewModelAtletaEditar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }

        public ViewModelPaisListado Pais { get; set; }
        public IEnumerable<ViewModelDisciplinaListado> Disciplinas { get; set; } = [];
    }
}
