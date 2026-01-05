namespace PokemonMlWebApp.Models;

public class Pokemon
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string? PrimaryType {get; set;}

    public int Height {get; set;}
    public int Weight {get; set;}
    
    public int Hp {get; set;}
    public int Defense {get; set;}

    public int Attack {get; set;}
    public int SpecialAttack {get; set;}
    public int SpecialDefense {get; set;}
    public int Speed {get; set;}
    
}