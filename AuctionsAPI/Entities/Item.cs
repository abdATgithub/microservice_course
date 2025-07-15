using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionsAPI.Entities;

/// <summary>
/// Represents an item entity in the auction system.
/// </summary>
[Table("Items")]
public class Item
{
    /// <summary>
    /// Gets or sets the unique identifier for the item.
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// Gets or sets the auction associated with this item.
    /// </summary>
    public Auction Auction { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the associated auction.
    /// </summary>
    public Guid AuctionId { get; set; }
}