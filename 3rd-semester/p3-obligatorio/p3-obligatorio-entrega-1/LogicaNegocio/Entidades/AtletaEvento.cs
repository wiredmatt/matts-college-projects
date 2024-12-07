public class AtletaEvento
{
    public int IdAtleta { get; set; }
    public int IdEvento { get; set; }
    public double Puntaje { get; set; }

    public AtletaEvento() { }

    public AtletaEvento(int idAtleta, int idEvento, double puntaje)
    {
        IdAtleta = idAtleta;
        IdEvento = idEvento;
        Puntaje = puntaje;

        Validar();
    }

    private void Validar()
    {
        if (Puntaje < 0)
        {
            throw new ArgumentException("El puntaje no puede ser negativo.");
        }
    }
}