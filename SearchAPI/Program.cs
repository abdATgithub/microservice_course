using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchAPI.Data;
using SearchAPI.Models;
using SearchAPI.Services;

namespace SearchAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());

        var app = builder.Build();

        app.UseAuthorization();

        app.MapControllers();

        app.Lifetime.ApplicationStarted.Register(async void () =>
        {
            try
            {
                await DbInitializer.Initialize(app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });

        await app.RunAsync();
    }

    /// <summary>
    /// Defines a policy for handling transient HTTP errors and retrying requests when specific conditions are met.
    /// The policy targets scenarios such as temporary network issues or retrying requests when
    /// the HTTP response status code is 404 (Not Found).
    /// </summary>
    /// <returns>A configured asynchronous retry policy for handling transient HTTP errors.</returns>
    static IAsyncPolicy<HttpResponseMessage> GetPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
}