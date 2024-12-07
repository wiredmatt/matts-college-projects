using WebApp.Models.Atletas;
using Newtonsoft.Json;
using WebApp.Models.Usuarios;

namespace WebApp.Services;

public static class AutenticacionService
{
    private static readonly HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5295/api/Autenticacion/")
    };

    private static class Rutas
    {
        public const string Login = "Login";
    }

    public static ViewModelUsuarioLogeadoConToken Login(ViewModelUsuarioLogin vmLogin)
    {
        Task<HttpResponseMessage> response = client.PostAsJsonAsync(Rutas.Login, vmLogin);
        response.Wait();
        HttpResponseMessage result = response.Result;

        Task<string> data = result.Content.ReadAsStringAsync();
        data.Wait();

        if (result.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<ViewModelUsuarioLogeadoConToken>(data.Result)!;

        var err = JsonConvert.DeserializeObject<CustomHTTPException>(data.Result) ?? new CustomHTTPException("Error desconocido");

        throw err;
    }
}