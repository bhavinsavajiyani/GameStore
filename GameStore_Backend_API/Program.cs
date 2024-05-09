using GameStore_Backend_API;
using GameStore_Backend_API.DTOs;
using Microsoft.AspNetCore.Mvc;

const string GetEndPointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

var app = builder.Build();

List<GameDTO> games = [
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

// GET all games in the collection (/games)
app.MapGet("games", () => games);

// GET a specified game from the collection (/games/1)
app.MapGet("games/{id}", (int id) =>
{
    GameDTO? game = games.Find(game => game.ID == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetEndPointName);

// POST new game to the collection (/games)
app.MapPost("games", (CreateGameDTO newGame) =>
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
app.MapPut("games/{id}", (int id, UpdateGameDTO updatedGame) => {
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
app.MapDelete("games/{id}", (int id) => {
    games.RemoveAll(game => game.ID == id);

    return Results.NoContent();
});

app.Run();
