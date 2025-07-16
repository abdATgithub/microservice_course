namespace SearchAPI.RequestHelpers;

/// <summary>
/// Represents the parameters for performing a search operation.
/// </summary>
public class SearchParams
{
    /// <summary>
    /// Gets or sets the search term used to filter or query the data.
    /// </summary>
    public string SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the current page number for paginated results.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items displayed per page in a paginated result.
    /// </summary>
    public int PageSize { get; set; } = 4;

    /// <summary>
    /// Gets or sets the seller associated with the search parameters.
    /// </summary>
    public string Seller { get; set; }

    /// <summary>
    /// Gets or sets the name or identifier of the winner associated with the search result.
    /// </summary>
    public string Winner { get; set; }

    /// <summary>
    /// Gets or sets the field used to determine the order of the search results.
    /// </summary>
    public string OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the criteria used to filter the search results.
    /// </summary>
    public string FilterBy { get; set; }
}