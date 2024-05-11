using GameStore_Frontend.Models;

namespace GameStore_Frontend.Clients;

public class GenresClient(HttpClient httpClient)
{
    public async Task<Genres[]> GetGenresAsync() =>
        await httpClient.GetFromJsonAsync<Genres[]>("genres") ?? [];
}
