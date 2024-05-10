﻿using GameStore_Backend_API.DTOs;
using GameStore_Backend_API.Entities;

namespace GameStore_Backend_API.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDTO game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreID = game.GenreID,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDTO game, int id)
    {
        return new Game()
        {
            ID = id,
            Name = game.Name,
            GenreID = game.GenreID,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDTO ToGameSummaryDTO(this Game game)
    {
        return new(
            game.ID,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDTO ToGameDetailsDTO(this Game game)
    {
        return new(
            game.ID,
            game.Name,
            game.GenreID,
            game.Price,
            game.ReleaseDate
        );
    }
}
