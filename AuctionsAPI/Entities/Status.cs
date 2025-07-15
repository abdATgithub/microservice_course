namespace AuctionsAPI.Entities;
/// <summary>
/// Represents the status of an auction.
/// </summary>
public enum Status
{
    /// <summary>
    /// The auction is currently live and accepting bids.
    /// </summary>
    Live,

    /// <summary>
    /// The auction has finished.
    /// </summary>
    Finished,

    /// <summary>
    /// The auction ended without meeting the reserve price.
    /// </summary>
    ReserveNotMet
}