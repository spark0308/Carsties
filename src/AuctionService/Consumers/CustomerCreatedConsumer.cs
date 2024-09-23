using System;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
{
    public async Task Consume(ConsumeContext<CustomerCreated> context)
    {
        Console.WriteLine("Consuming Male Customer Created -->");
        Console.WriteLine(context.Message.Name);
        Console.WriteLine(context.Message.Age);

        await Task.CompletedTask;
    }
}
