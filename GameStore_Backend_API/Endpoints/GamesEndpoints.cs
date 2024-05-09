using GameStore_Backend_API.DTOs;

namespace GameStore_Backend_API.Endpoints;

public static class GamesEndpoints
{
    private const string GetEndPointName = "GetGame";

    private static readonly List<GameDTO> games =
    [
        new(
            1,
            "Street Fighter II",
            "Action",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new(
            2,
            "Final Fantasy XIV",
            "RPG",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new(
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 9, 27)
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET all games in the collection (/games)
        group.MapGet("/", () => games);

        // GET a specified game from the collection (/games/1)
        group.MapGet("/{id}", (int id) =>
        {
            GameDTO? game = games.Find(game => game.ID == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetEndPointName);

        // POST new game to the collection (/games)
        group.MapPost("/", (CreateGameDTO newGame) =>
        {
            GameDTO game = new(
                games.Count() + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetEndPointName, new {id = game.ID}, game);
        });

        // PUT -> edit & update specified game from the collection
        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) => {
            int index = games.FindIndex(game => game.ID == id);

            if(index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDTO(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE specified game from the collection
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.ID == id);

            return Results.NoContent();
        });

        return group;
    }
}
