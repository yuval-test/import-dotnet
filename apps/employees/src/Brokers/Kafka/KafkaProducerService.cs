using Employees.Brokers.Infrastructure;

namespace Employees.Brokers.Kafka;

public class KafkaProducerService : InternalProducer
{
    public KafkaProducerService(string bootstrapServers)
        : base(bootstrapServers) { }
}
