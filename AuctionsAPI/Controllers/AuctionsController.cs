using AuctionsAPI.Data;
using AuctionsAPI.DTOs;
using AuctionsAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionsAPI.Controllers;

/// <summary>
/// Controller class for handling auction-related operations, which include retrieving all auctions,
/// fetching auction details by ID, creating new auctions, and updating existing auction entries.
/// </summary>
[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    /// <summary>
    /// Represents the database context used for accessing and managing auction-related data within the application.
    /// Provides access to the underlying database and facilitates CRUD operations on auction data.
    /// </summary>
    private readonly AuctionDbContext  _context;

    /// <summary>
    /// Represents the object mapper used for transforming data between domain entities and Data Transfer Objects (DTOs).
    /// Facilitates object-to-object mapping, reducing code duplication and improving maintainability in the process of data conversion.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Controller class for managing auction operations, such as retrieving, creating, updating auctions
    /// and fetching auction details by their identifier.
    /// </summary>
    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of all auctions, including their associated item details,
    /// sorted by the make of the item.
    /// </summary>
    /// <returns>A list of auction data transfer objects (AuctionDto) containing auction details.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();
        
        return _mapper.Map<List<AuctionDto>>(await auctions);
    }

    /// <summary>
    /// Retrieves the auction details by the specified auction identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the auction to retrieve.</param>
    /// <returns>An <see cref="AuctionDto"/> representing the details of the auction, or a NotFound result if no auction exists with the specified identifier.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound();
        }
        return _mapper.Map<AuctionDto>(auction);
    }

    /// <summary>
    /// Creates a new auction entry and stores it in the database.
    /// </summary>
    /// <param name="auctionDto">An object containing the details of the auction to be created, such as item information and reserve price.</param>
    /// <returns>An ActionResult containing the newly created auction details represented as an AuctionDto.
    /// Returns BadRequest if the auction cannot be saved, or CreatedAtAction if the creation is successful.</returns>
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);
        //TODO: add current user as seller
        auction.Seller = "test";
        _context.Auctions.Add(auction);
        var result = await _context.SaveChangesAsync() > 0;
        
        if (!result) return BadRequest("Could not save changes to the database!");
        
        return CreatedAtAction(nameof(GetAuctionById), new {id = auction.Id}, _mapper.Map<AuctionDto>(auction));
    }

    /// <summary>
    /// Updates the details of an existing auction, including updating the item information associated with the auction.
    /// </summary>
    /// <param name="id">The unique identifier of the auction to be updated.</param>
    /// <param name="updateAuctionDto">An object containing the updated auction details.</param>
    /// <returns>An ActionResult indicating the result of the update operation.
    /// Returns NotFound if the auction does not exist, Ok if the update is successful,
    /// or BadRequest if the changes could not be saved to the database.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (auction == null) return NotFound();
        
        // TODO: check seller == username
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        
        var result = await _context.SaveChangesAsync() > 0;
        
        if (!result) return BadRequest("Could not update changes to the database!");
        
        return Ok();
    }

    /// <summary>
    /// Deletes an auction with the specified identifier from the database.
    /// If the auction is not found, a 404 Not Found response is returned.
    /// If the deletion fails to persist changes, a 400 Bad Request response is returned.
    /// </summary>
    /// <param name="id">The unique identifier of the auction to be deleted.</param>
    /// <returns>
    /// An HTTP response indicating the outcome of the delete operation.
    /// Returns 200 OK if the auction is successfully deleted,
    /// 404 Not Found if the auction is not found,
    /// or 400 Bad Request if the operation fails to save changes to the database.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);
        if (auction == null) return NotFound();
        _context.Auctions.Remove(auction);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not delete changes to the database!");
        return Ok();
    }
}