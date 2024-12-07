using Compartido.DTOs.Disciplinas;
using Compartido.DTOs.Paises;
using LogicaNegocio.Enums;

namespace Compartido.DTOs.Atletas
{
    public class DTOAtletaListado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }
        public DTOPaisListado Pais { get; set; }
        public IEnumerable<DTODisciplinaListado> Disciplinas { get; set; } = [];
    }
}
