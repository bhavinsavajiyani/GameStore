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

    private readonly Genres[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. games];

    public void AddGame(GameDetails game)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreID);
        var genre = genres.Single(genre => genre.ID == int.Parse(game.GenreID));

        var gameSummary = new GameSummary
        {
            ID = games.Count() + 1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };

        games.Add(gameSummary);
    }

    public GameDetails GetGame(int id)
    {
        GameSummary? game = games.Find(game => game.ID == id);
        ArgumentNullException.ThrowIfNull(game);
        
        var genre = genres.Single(genre => string.Equals(
            genre.Name,
            game.Genre,
            StringComparison.OrdinalIgnoreCase)
        );

        return new GameDetails {
            ID = id,
            Name = game.Name,
            GenreID = genre.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}
