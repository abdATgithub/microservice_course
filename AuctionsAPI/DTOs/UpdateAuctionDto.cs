namespace AuctionsAPI.DTOs;

public class UpdateAuctionDto
{
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
    public int? Year { get; set; }

    /// <summary>
    /// Gets or sets the mileage of the item.
    /// </summary>
    public int? Mileage { get; set; }

    /// <summary>
    /// Gets or sets the color of the item.
    /// </summary>
    public string Color { get; set; }
}