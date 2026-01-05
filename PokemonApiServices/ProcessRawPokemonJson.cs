using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using PokemonMlWebApp.Models;

namespace PokemonMlWebApp.PokemonApiServices;

public class ProcessRawPokemonJson
{
    public async Task<int> ProcessRawJson()
    {
        string unprocessedDirname = Path.Join("Data", "Unprocessed");
        string processedDirname = Path.Join("Data", "Processed");
        Directory.CreateDirectory(processedDirname);

        if (!Directory.Exists(unprocessedDirname)) return 0;

        if (!Directory.EnumerateFiles(unprocessedDirname, "*.json").Any()) return 0;

        List<Pokemon> pokemons = new List<Pokemon>();

        foreach (var file in Directory.EnumerateFiles(unprocessedDirname, "*.json"))
        {
            var jsonString = await File.ReadAllTextAsync(file);
            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;

            Pokemon pokemon = new Pokemon()
            {
                Id = root.GetProperty("id").GetInt32(),
                Name = root.GetProperty("name").GetString() ?? "Unknown",
                PrimaryType = root.GetProperty("types")[0].GetProperty("type").GetProperty("name").GetString() ?? "Unknown"
            };

            foreach (var stat in root.GetProperty("stats").EnumerateArray())
            {
                int value = stat.GetProperty("base_stat").GetInt32();
                string name = stat.GetProperty("stat").GetProperty("name").GetString() ?? "Unknown";

                switch (name)
                {
                    case "hp":
                        pokemon.Hp = value;
                        break;
                    case "attack":
                        pokemon.Attack = value;
                        break;
                    case "defense":
                        pokemon.Defense = value;
                        break;
                    case "special-attack":
                        pokemon.SpecialAttack = value;
                        break;
                    case "special-defense":
                        pokemon.SpecialDefense = value;
                        break;
                    case "speed":
                        pokemon.Speed = value;
                        break;
                }
            }

            pokemons.Add(pokemon);
        }

        string columns = "Id,Name,PrimaryType,Hp,Attack,Defense,SpecialAttack,SpecialDefense,Speed";

        var sb = new StringBuilder();

        sb.AppendLine(columns);

        foreach (Pokemon pk in pokemons)
        {
            sb.AppendLine(string.Join(",", pk.Id, pk.Name, pk.PrimaryType, pk.Hp, pk.Attack, pk.Defense, 
            pk.SpecialAttack, pk.SpecialDefense, pk.Speed));
        }
        
        await File.WriteAllTextAsync(Path.Join(processedDirname, "ProcessedPokemonData.csv"), sb.ToString());

        return 1;
    }
}