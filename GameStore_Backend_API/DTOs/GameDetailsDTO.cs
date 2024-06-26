﻿namespace GameStore_Backend_API.DTOs;

public record class GameDetailsDTO(
    int ID,
    string Name,
    int GenreID,
    decimal Price,
    DateOnly ReleaseDate);
