using System;
using Dapr.Client;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

Console.WriteLine("Starting Client");
DaprClient client = null;

bool init = false;
while (!init)
{
    try
    {
        client = new DaprClientBuilder().Build();
        init = true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

for (int i = 1; i <= 1000; i++)
{
    var order = new Order(i);

    try
    {
        await client.PublishEventAsync("orderpubsub", "orders", order);
    }
    catch(Exception e)
    {
        Console.WriteLine(e.Message);
    }
    // Publish an event/message using Dapr PubSub
    Console.WriteLine("Published data: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);
