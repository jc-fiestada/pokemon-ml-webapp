using PokemonMlWebApp.PokemonApiServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<LoadDataFromPokemonApi>();
var app = builder.Build();

app.MapGet("/load/pokemon/unprocessed", async (LoadDataFromPokemonApi service) =>
{
    int success = await service.LoadPokemonJson(50);

    if (success != 1) return Results.UnprocessableEntity("Json Files Already Exist's");
    return Results.Ok("Pokemon Successfully Loaded");
});

app.MapGet("/load/pokemon/processed", async () =>
{
    ProcessRawPokemonJson service = new ProcessRawPokemonJson();
    int success = await service.ProcessRawJson();
    if (success != 1) return Results.InternalServerError("Server error");
    return Results.Ok("Data has been processed");
});

app.Run();
