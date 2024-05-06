using GameStore_Frontend.Models;

namespace GameStore_Frontend.Clients;

public class GenresClient
{
    private readonly Genres[] genres = [
        new()
        {
            ID = 1,
            Name = "Action",
        },
        new()
        {
            ID = 2,
            Name = "RPG",
        },
        new()
        {
            ID = 3,
            Name = "Sports",
        },
        new()
        {
            ID = 4,
            Name = "Puzzle",
        },
        new()
        {
            ID = 5,
            Name = "MOBA",
        },
        new()
        {
            ID = 6,
            Name = "Racing",
        },
    ];

    public Genres[] GetGenres() => genres;
}
