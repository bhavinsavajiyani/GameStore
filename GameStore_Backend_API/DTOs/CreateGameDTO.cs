﻿using System.ComponentModel.DataAnnotations;

namespace GameStore_Backend_API.DTOs;

public record class CreateGameDTO
(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1, 100)]              decimal Price,
    DateOnly                     ReleaseDate
);