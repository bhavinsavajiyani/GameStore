using GameStore_Frontend.Models;

namespace GameStore_Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{
    public async Task<GameSummary[]> GetGamesAsync() =>
        await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

    public async Task AddGameAsync(GameDetails game) =>
        await httpClient.PostAsJsonAsync("games", game);

    public async Task<GameDetails> GetGameAsync(int id) =>
        await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ??
        throw new Exception("Couldn't Find Game...!");

    public async Task UpdateGameAsync(GameDetails updatedGame) => 
        await httpClient.PutAsJsonAsync($"games/{updatedGame.ID}", updatedGame);

    public async Task DeleteGameAsync(int id) =>
        await httpClient.DeleteAsync($"games/{id}");
}
