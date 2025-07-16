using MongoDB.Entities;

namespace SearchAPI.Models;

/// <summary>
/// Represents an item listed for auction, including details about its seller, status,
/// bidding information, and vehicle-specific attributes.
/// </summary>
public class Item : Entity
{
    /// <summary>
    /// Gets or sets the minimum price required for the auction to be successful.
    /// </summary>
    public int ReservePrice { get; set; }

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
    public int SoldAmount { get; set; }

    /// <summary>
    /// Gets or sets the current highest bid for the auction.
    /// </summary>
    public int CurrentHighBid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction was created (in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction was last updated (in UTC).
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction ends.
    /// </summary>
    public DateTime AuctionEnd { get; set; }

    /// <summary>
    /// Gets or sets the current status of the auction.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Gets or sets the make (manufacturer) of the item.
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// Gets or sets the model of the item.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the manufacturing year of the item.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the mileage of the item.
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// Gets or sets the color of the item.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the URL of the item's image.
    /// </summary>
    public string ImageUrl { get; set; }
}