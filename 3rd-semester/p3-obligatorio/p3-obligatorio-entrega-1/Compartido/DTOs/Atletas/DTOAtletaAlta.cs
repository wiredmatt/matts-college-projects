using LogicaNegocio.Enums;

namespace Compartido.DTOs.Atletas
{
    public class DTOAtletaAlta
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Sexo Sexo { get; set; }
        public int IdPais { get; set; }
        public List<int> IdsDisciplinas { get; set; } = [];
    }
}
