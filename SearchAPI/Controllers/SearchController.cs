using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchAPI.Models;
using SearchAPI.RequestHelpers;

namespace SearchAPI.Controllers;

/// <summary>
/// Controller responsible for handling search-related operations.
/// </summary>
[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    /// <summary>
    /// Searches for auction items based on the specified parameters such as search term, filters, and sorting preferences.
    /// </summary>
    /// <param name="searchParams">The search parameters including search term, page number, page size, seller, winner, order by, and filters.</param>
    /// <returns>A task that represents the asynchronous operation, containing an action result with a list of items matching the search criteria, along with pagination and total count information.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item, Item>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };

        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x =>
                x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
        };

        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }
        
        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }
        
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        
        var result = await query.ExecuteAsync();
        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}