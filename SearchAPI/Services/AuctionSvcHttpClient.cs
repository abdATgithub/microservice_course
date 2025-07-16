using MongoDB.Entities;
using SearchAPI.Models;

namespace SearchAPI.Services;

/// <summary>
/// Represents a client for interacting with a remote auction service, specifically designed to
/// retrieve and manage auction item data for the local search database.
/// </summary>
public class AuctionSvcHttpClient
{
    /// <summary>
    /// Represents the underlying HTTP client used for sending requests to the auction service API.
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// Represents the configuration settings for the application, providing access to
    /// key-value pairs from the application's configuration sources.
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    /// Provides methods for interacting with the remote auction service, including retrieving auction items
    /// for updating the local search database.
    /// </summary>
    public AuctionSvcHttpClient(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Retrieves a list of auction items from the remote service by querying the latest update timestamp
    /// from the database and fetching items updated since that timestamp.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of items.</returns>
    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(a => a.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();
        
        return await _client.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" +
                                                          lastUpdated);
    }
}