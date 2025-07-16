using System.ComponentModel.DataAnnotations;

namespace AuctionsAPI.DTOs;

/// <summary>
/// Represents the data transfer object used to create a new auction entry.
/// </summary>
/// <remarks>
/// This DTO contains all the required fields necessary for creating an auction,
/// including details about the item to be auctioned and the auction's parameters.
/// </remarks>
public class CreateAuctionDto
{
    /// <summary>
    /// Gets or sets the make (manufacturer) of the item.
    /// </summary>
    [Required]
    public string Make { get; set; }

    /// <summary>
    /// Gets or sets the model of the item.
    /// </summary>
    [Required]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the manufacturing year of the item.
    /// </summary>
    [Required]
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the mileage of the item.
    /// </summary>
    [Required]
    public int Mileage { get; set; }

    /// <summary>
    /// Gets or sets the color of the item.
    /// </summary>
    [Required]
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the URL of the item's image.
    /// </summary>
    [Required]
    public string ImageUrl { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum price required for the auction to be successful.
    /// </summary>
    [Required]
    public int ReservePrice { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the auction ends.
    /// </summary>
    [Required]
    public DateTime AuctionEnd { get; set; }
}