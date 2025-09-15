using OnSalesStore.ECommerce.Domain.Entities;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
    }
}
