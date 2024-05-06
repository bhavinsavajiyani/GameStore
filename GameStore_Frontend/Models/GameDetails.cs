namespace GameStore_Frontend.Models;

public class GameDetails
{
    public int ID {get; set;}
    public required string Name {get; set;}
    public string? GenreID {get; set;}
    public decimal Price {get; set;}
    public DateOnly ReleaseDate {get; set;}
}
