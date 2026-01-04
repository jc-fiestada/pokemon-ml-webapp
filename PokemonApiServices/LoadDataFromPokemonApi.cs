using System.Text.Json;

namespace PokemonMlWebApp.PokemonApiServices;

public class LoadDataFromPokemonApi
{
    private readonly HttpClient _httpClient;

    public LoadDataFromPokemonApi(HttpClient client)
    {
        _httpClient = client;
        _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<string> LoadPokemonJson(int pokemonQuantity)
    {
        string dirname = Path.Combine("Data", "Unprocessed");
        Directory.CreateDirectory(dirname);
        
        for (int id = 1; id <= pokemonQuantity; id++)
        {
            var response = await _httpClient.GetAsync($"pokemon/{id}");

            if (!response.IsSuccessStatusCode)
            {
                continue;
            }

            var json = await response.Content.ReadAsStringAsync();

            string filepath = Path.Combine(dirname, $"Pokemon {id}.json");
            

            File.WriteAllText(filepath, json);
        }

        return "Pokemon data has been loaded successfully";
    }
}