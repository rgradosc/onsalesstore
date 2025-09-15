using OnSalesStore.ECommerce.Application.Interfaces.Infrastructure;
using MassTransit;

namespace OnSalesStore.ECommerce.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public void Publish<T>(T @event)
        {
        }
    }
}
