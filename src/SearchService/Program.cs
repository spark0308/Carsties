using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using RabbitMQ.Client;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
        x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
        x.UsingRabbitMq((context, cfg) =>
        {
                cfg.ReceiveEndpoint("search-auction-create", e =>
                {
                        e.UseMessageRetry(r => r.Interval(5, 5));

                        e.ConfigureConsumer<AuctionCreatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("female-customer", e =>
                {
                        e.ConfigureConsumeTopology = false;
                        e.Consumer<CustomerCreatedConsumer>();

                        e.Bind("customerEvent", b =>
                        {
                                b.RoutingKey = "Female";
                                b.ExchangeType = ExchangeType.Direct;
                        });
                });
                cfg.ConfigureEndpoints(context);
        });
});


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
        try
        {
                await DbInitializer.Init(app);
        }
        catch (Exception ex)
        {
                Console.WriteLine(ex);
        }
});


app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
        => HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
