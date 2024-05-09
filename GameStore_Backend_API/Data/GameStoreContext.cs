using GameStore_Backend_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore_Backend_API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}
