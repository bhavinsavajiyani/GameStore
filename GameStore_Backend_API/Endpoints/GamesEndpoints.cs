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
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                     .Include(game => game.Genre)
                     .Select(game => game.ToGameSummaryDTO())
                     .AsNoTracking()
                     .ToListAsync());

        // GET a specified game from the collection (/games/1)
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDTO());
        })
        .WithName(GetEndPointName);

        // POST new game to the collection (/games)
        group.MapPost("/", async (CreateGameDTO newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetEndPointName, new {id = game.ID}, game.ToGameDetailsDTO());
        });

        // PUT -> edit & update specified game from the collection
        group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if(existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE specified game from the collection
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                     .Where(game => game.ID == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
