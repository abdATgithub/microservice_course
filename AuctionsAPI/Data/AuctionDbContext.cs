using AuctionsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionsAPI.Data;

public class AuctionDbContext : DbContext
{
    public DbSet<Auction> Auctions { get; set; }
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    }
}