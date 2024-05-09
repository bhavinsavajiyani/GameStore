namespace GameStore_Backend_API.DTOs;

public record class UpdateGameDTO(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
