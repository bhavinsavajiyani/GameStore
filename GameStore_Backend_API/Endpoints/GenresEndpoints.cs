using GameStore_Backend_API.Data;
using GameStore_Backend_API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore_Backend_API.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
                           .Select(genre => genre.ToDTO())
                           .AsNoTracking()
                           .ToListAsync());

        return group;
    }
}
