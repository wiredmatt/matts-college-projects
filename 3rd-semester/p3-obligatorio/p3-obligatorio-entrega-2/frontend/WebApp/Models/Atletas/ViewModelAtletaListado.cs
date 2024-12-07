using WebApp.Models.Disciplinas;
using WebApp.Models.Paises;
using WebApp.Enums;

namespace WebApp.Models.Atletas
{
    public class ViewModelAtletaListado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }
        public ViewModelPaisListado Pais { get; set; }
        public IEnumerable<ViewModelDisciplinaListado> Disciplinas { get; set; } = [];
    }
}
