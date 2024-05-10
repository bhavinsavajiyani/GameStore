using GameStore_Backend_API.Data;
using GameStore_Backend_API.DTOs;
using GameStore_Backend_API.Entities;
using GameStore_Backend_API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore_Backend_API.Endpoints;

public static class GamesEndpoints
{
    private const string GetEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET all games in the collection (/games)
        group.MapGet("/", (GameStoreContext dbContext) =>
            dbContext.Games
                     .Include(game => game.Genre)
                     .Select(game => game.ToGameSummaryDTO())
                     .AsNoTracking());

        // GET a specified game from the collection (/games/1)
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDTO());
        })
        .WithName(GetEndPointName);

        // POST new game to the collection (/games)
        group.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetEndPointName, new {id = game.ID}, game.ToGameDetailsDTO());
        });

        // PUT -> edit & update specified game from the collection
        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if(existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE specified game from the collection
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games
                     .Where(game => game.ID == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
