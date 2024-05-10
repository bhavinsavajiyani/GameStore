using GameStore_Backend_API.DTOs;
using GameStore_Backend_API.Entities;

namespace GameStore_Backend_API.Mapping;

public static class GenreMapping
{
    public static GenreDTO ToDTO(this Genre genre)
    {
        return new GenreDTO(genre.ID, genre.Name);
    }
}
