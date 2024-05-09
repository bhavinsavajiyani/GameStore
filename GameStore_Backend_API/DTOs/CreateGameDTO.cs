namespace GameStore_Backend_API.DTOs;

public record class CreateGameDTO(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);
