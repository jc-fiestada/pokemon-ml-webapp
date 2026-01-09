using PokemonMlWebApp.Models;
namespace PokemonMlWebApp.PokemonApiServices;

public class LoadPokemonPredicion
{
    private readonly HttpClient _httpclient;

    public LoadPokemonPredicion(HttpClient client)
    {
        _httpclient = client;
        _httpclient.BaseAddress = new Uri("http://127.0.0.1:8000/");
        _httpclient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<List<PokemonModelTestResponse>> LoadTestResults()
    {
        List<PokemonModelTestResponse>? response = await _httpclient.GetFromJsonAsync<List<PokemonModelTestResponse>>("predict/pokemon-type");

        return response;
    }

    

    
}