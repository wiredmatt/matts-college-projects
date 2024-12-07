using WebApp.Models.Atletas;
using Newtonsoft.Json;

namespace WebApp.Services;

public static class AtletaService
{
    private static readonly HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5295/api/Atleta/")
    };

    private static class Rutas
    {
        public const string BuscarPorIdDisciplina = "BuscarPorIdDisciplina/";
    }

    public static IEnumerable<ViewModelAtletaListado> AtletaBuscarPorIdDisciplina(int id)
    {
        Task<HttpResponseMessage> response = client.GetAsync(Rutas.BuscarPorIdDisciplina + id);
        response.Wait();
        HttpResponseMessage result = response.Result;

        Task<string> data = result.Content.ReadAsStringAsync();
        data.Wait();

        if (result.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<IEnumerable<ViewModelAtletaListado>>(data.Result)!;

        var err = JsonConvert.DeserializeObject<CustomHTTPException>(data.Result) ?? new CustomHTTPException("Error desconocido");

        throw err;
    }
}