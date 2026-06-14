using Confluent.Kafka;
using System.Text.Json;
using TaxService.Application.Interfaces;

namespace TaxService.Infrastructure.Kafka;

public class KafkaTaxEventPublisher : ITaxEventPublisher
{
    private readonly IProducer<Null, string> _producer;

    public KafkaTaxEventPublisher()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishTaxCalculatedAsync(int employeeId, decimal totalTax)
    {
        try
        {
            var payload = JsonSerializer.Serialize(new { employeeId, totalTax, timestamp = DateTime.UtcNow });
            await _producer.ProduceAsync("tax-calculated", new Message<Null, string> { Value = payload });
        }
        catch
        {
            // Kafka not running — swallow for prototype so the request still succeeds
        }
    }
}