using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IUserRepository Users { get; }

        ICategoryRepository Categories { get; }

        IDiscountRepository Discounts { get; }

        Task<int> Save(CancellationToken cancellationToken);
    }
}
