using GameStore_Backend_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore_Backend_API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new{ID = 1, Name = "Action"},
            new{ID = 2, Name = "RPG"},
            new{ID = 3, Name = "Sports"},
            new{ID = 4, Name = "Casual"},
            new{ID = 5, Name = "Racing"}
        );
    }
}
