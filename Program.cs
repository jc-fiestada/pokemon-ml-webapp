using PokemonMlWebApp.PokemonApiServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<LoadDataFromPokemonApi>();
var app = builder.Build();

app.MapGet("/load/pokemon/unprocessed", async (LoadDataFromPokemonApi service) =>
{
    await service.LoadPokemonJson(50);
    return Results.Ok("Pokemon Successfully Loaded");
});

app.Run();
