namespace Compartido.DTOs.Atletas
{
    public class DTOAtletaEditar
    {
        // for now the requirement is to only edit Disciplinas, no need for these
        // public string Nombre { get; set; }
        // public string Apellido { get; set; }
        // public Sexo Sexo { get; set; }
        // public int IdPais { get; set; }
        public List<int> IdsDisciplinas { get; set; } = [];
    }
}
