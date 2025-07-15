namespace AuctionsAPI.Entities;

/// <summary>
/// Represents an auction entity with details about the auction, seller, winner, item, and status.
/// </summary>
public class Auction
{
    /// <summary>
    /// Gets or sets the unique identifier for the auction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the minimum price required for the auction to be successful.
    /// </summary>
    public int ReservePrice { get; set; } = 0;

    /// <summary>
    /// Gets or sets the username or identifier of the seller.
    /// </summary>
    public string Seller { get; set; }

    /// <summary>
    /// Gets or sets the username or identifier of the winning bidder.
    /// </summary>
    public string Winner { get; set; }

    /// <summary>
    /// Gets or sets the amount for which the item was sold, if applicable.
    /// </summary>
    public int? SoldAmount { get; set; }

    /// <summary>
    /// Gets or sets the current highest bid for the auction.
    /// </summary>
    public int? CurrentHighBid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction was created (in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the auction was last updated (in UTC).
    /// </summary>
    public DateTime UpdatedAt { get; set; } =  DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the auction ends.
    /// </summary>
    public DateTime AuctionEnd { get; set; }

    /// <summary>
    /// Gets or sets the current status of the auction.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the item being auctioned.
    /// </summary>
    public Item Item { get; set; }
}