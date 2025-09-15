using OnSalesStore.ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface IDiscountRepository : IGenericRepository<Discount>, IPaginationRepository<Discount>
    {
        Task<Discount> SelectAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Discount>> SelectAllAsync(CancellationToken cancellationToken);
    }
}
