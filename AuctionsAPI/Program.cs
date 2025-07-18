using AuctionsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AuctionDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("AuctionsDBConnection"));
        });
        
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        try
        {
            DbInitializer.Initialize(app);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        app.Run();
    }
}