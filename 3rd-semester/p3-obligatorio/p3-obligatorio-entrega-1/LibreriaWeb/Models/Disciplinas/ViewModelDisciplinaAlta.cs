using System.ComponentModel;

namespace LibreriaWeb.Models.Disciplinas
{
    public class ViewModelDisciplinaAlta
    {
        public string Nombre { get; set; }

        [DisplayName("Codigo")]
        public int Id { get; set; }

        [DisplayName("Año")]
        public int Ano { get; set; }
    }
}
