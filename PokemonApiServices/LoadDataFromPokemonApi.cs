using System.Text.Json;

namespace PokemonMlWebApp.PokemonApiServices;

public class LoadDataFromPokemonApi
{
    static string dirname = Path.Combine("Data", "Unprocessed");

    private readonly HttpClient _httpClient;

    public LoadDataFromPokemonApi(HttpClient client)
    {
        _httpClient = client;
        _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    private int DeleteFiles()
    {
        if (!Directory.Exists(dirname) || !Directory.EnumerateFiles(dirname).Any()) return 0;
        
        foreach (var file in Directory.EnumerateFiles(dirname))
        {
            File.Delete(file);
        }
        return 1;
    }

    public async Task<int> LoadPokemonJson(int pokemonQuantity)
    {
        Directory.CreateDirectory(dirname);

        if (Directory.EnumerateFiles(dirname).Take(pokemonQuantity).Count() == pokemonQuantity)
        {
            return 0;
        }

        DeleteFiles();
        
        for (int id = 1; id <= pokemonQuantity; id++)
        {
            var response = await _httpClient.GetAsync($"pokemon/{id}");

            if (!response.IsSuccessStatusCode) continue;
            var json = await response.Content.ReadAsStringAsync();

            string filepath = Path.Combine(dirname, $"Pokemon {id}.json");
            

            File.WriteAllText(filepath, json);
        }

        return 1;
    }

    
}