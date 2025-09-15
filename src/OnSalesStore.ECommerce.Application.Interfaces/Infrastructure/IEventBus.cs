namespace OnSalesStore.ECommerce.Application.Interfaces.Infrastructure
{
    public interface IEventBus
    {
        void Publish<T>(T @event);
    }
}
