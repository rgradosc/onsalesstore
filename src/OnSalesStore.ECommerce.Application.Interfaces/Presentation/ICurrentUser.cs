namespace OnSalesStore.ECommerce.Application.Interfaces.Presentation
{
    public interface ICurrentUser
    {
        public string UserId { get; }

        public string UserName { get; }
    }
}
