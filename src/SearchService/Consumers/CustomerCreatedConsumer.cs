using System;
using Contracts;
using MassTransit;

namespace SearchService.Consumers;

public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
{
    public async Task Consume(ConsumeContext<CustomerCreated> context)
    {
         Console.WriteLine("Consuming Female Customer Created -->");
        Console.WriteLine(context.Message.Name);
        Console.WriteLine(context.Message.Age);

        await Task.CompletedTask;
    }
}
