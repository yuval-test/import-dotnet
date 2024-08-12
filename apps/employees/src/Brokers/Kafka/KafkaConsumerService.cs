using DotnetService.Brokers.Infrastructure;
using Employees.Brokers.Infrastructure;
using Employees.Brokers.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Brokers.Kafka;

public class KafkaConsumerService : KafkaConsumerService<KafkaMessageHandlersController>
{
    public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory, KafkaOptions kafkaOptions)
        : base(serviceScopeFactory, kafkaOptions) { }
}
