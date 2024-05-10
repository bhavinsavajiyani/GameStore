using GameStore_Backend_API;
using GameStore_Backend_API.Data;
using GameStore_Backend_API.Endpoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
