using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface IPaginationRepository<T> where T : class
    {
        IEnumerable<T> SelectAllWithPagination(int pageNumber, int pageSize);

        int Count();

        Task<IEnumerable<T>> SelectAllWithPaginationAsync(int pageNumber, int pageSize);

        Task<int> CountAsync();
    }
}
