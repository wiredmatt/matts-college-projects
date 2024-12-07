public class AtletaDisciplina
{
    public int IdAtleta { get; set; }
    public int IdDisciplina { get; set; }
    public AtletaDisciplina() { }

    public AtletaDisciplina(int idAtleta, int idDisciplina)
    {
        IdAtleta = idAtleta;
        IdDisciplina = idDisciplina;

        Validar();
    }

    private void Validar()
    {
    }
}