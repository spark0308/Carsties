using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using ThirdService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Send<CustomerCreated>(x =>
        {
            x.UseRoutingKeyFormatter(context => context.Message.Gender);
        });
        cfg.Message<CustomerCreated>(x => x.SetEntityName("customerEvent"));
        cfg.Publish<CustomerCreated>(x => x.ExchangeType = ExchangeType.Direct);

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.Init(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
