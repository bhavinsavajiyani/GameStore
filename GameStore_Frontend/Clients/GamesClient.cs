using GameStore_Frontend.Models;

namespace GameStore_Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games = [
        new() {
            ID = 1,
            Name = "Street Fighter II",
            Genre = "Action",
            Price = 19.99M,
            ReleaseDate = new DateOnly(1999, 7, 15)
        },
        new() {
            ID = 2,
            Name = "Final Fantasy XIV",
            Genre = "RPG",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new() {
            ID = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 9, 27)
        }
    ];

    public GameSummary[] GetGames() => [.. games];
}
