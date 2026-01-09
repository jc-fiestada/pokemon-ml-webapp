namespace PokemonMlWebApp.Models;

public class PokemonModelTestResponse
{
    public string? ModelName {get; set;}
    public int[][] ConfusionMetrics {get; set;}
    public double AccuracyScore {get; set;}
    public double PrecisionScore {get; set;}
    public double RecallScore {get; set;}
    public double F1Score {get; set;}
}