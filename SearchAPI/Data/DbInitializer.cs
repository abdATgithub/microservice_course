using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchAPI.Models;
using SearchAPI.Services;

namespace SearchAPI.Data;

/// <summary>
/// Provides functionality to initialize the database for the application, including
/// setting up MongoDB connection, creating indexes for the item collection,
/// and seeding initial data if required.
/// </summary>
public class DbInitializer
{
    /// <summary>
    /// Initializes the database for the application by setting up the MongoDB connection,
    /// creating necessary indexes for the item collection, and seeding initial data if required.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to configure the application and its dependencies.</param>
    /// <returns>A task that represents the asynchronous operation of database initialization.</returns>
    public static async Task Initialize(WebApplication app)
    {
        await DB.InitAsync("SearchDB", MongoClientSettings
            .FromConnectionString(app.Configuration
                .GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        /* if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            
            var itemData = await File.ReadAllTextAsync("Data/auctions.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
            
            await DB.SaveAsync(items);
        } */
        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();
        var items = await httpClient.GetItemsForSearchDb();
        Console.WriteLine($"{items.Count} items retrieved from remote auctions-service");
        if (items.Count > 0) await DB.SaveAsync(items);
    }
}