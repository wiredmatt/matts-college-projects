using WebApp.Models.Eventos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace WebApp.Services;

public static class EventoService
{
    private static readonly HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5295/api/Evento/")
    };

    private static class Rutas
    {
        public const string BuscarPorFiltros = "";
    }

    public static IEnumerable<ViewModelEventoListado> EventoBuscarPorFiltros(
        int? idDisciplina,
        DateOnly? fechaInicio,
        DateOnly? fechaFin,
        string? nombreEvento,
        double? puntajeMinimo,
        double? puntajeMaximo,
        string bearerToken
    )
    {
        string query = "?";

        if (idDisciplina != null)
            query += $"idDisciplina={idDisciplina}&";
        if (fechaInicio != null)
            query += $"fechaInicio={fechaInicio}&";
        if (fechaFin != null)
            query += $"fechaFin={fechaFin}&";
        if (nombreEvento != null)
            query += $"nombreEvento={nombreEvento}&";
        if (puntajeMinimo != null)
            query += $"puntajeMinimo={puntajeMinimo}&";
        if (puntajeMaximo != null)
            query += $"puntajeMaximo={puntajeMaximo}&";

        if (query.EndsWith('&') || query.EndsWith('?'))
            query = query.Remove(query.Length - 1);

        var request = new HttpRequestMessage(HttpMethod.Get, Rutas.BuscarPorFiltros + query);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        Task<HttpResponseMessage> response = client.SendAsync(request);
        response.Wait();
        HttpResponseMessage result = response.Result;

        Task<string> data = result.Content.ReadAsStringAsync();
        data.Wait();

        if (result.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<IEnumerable<ViewModelEventoListado>>(data.Result)!;

        if (result.StatusCode == HttpStatusCode.Unauthorized) // [Authorize] causa que la api throwee 401 vacio.
            throw new CustomHTTPException("No autorizado! Debe logearse para ver resultados.");

        var err = JsonConvert.DeserializeObject<CustomHTTPException>(data.Result) ?? new CustomHTTPException("Error desconocido");

        throw err;
    }
}