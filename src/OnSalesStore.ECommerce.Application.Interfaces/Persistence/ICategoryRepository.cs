using OnSalesStore.ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> SelectAllAsync();
    }
}
