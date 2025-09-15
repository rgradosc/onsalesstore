using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Infrastructure
{
    public interface INotification
    {
        Task<bool> SendMailAsync(string subject, string body, CancellationToken cancellationToken = new());
    }
}
