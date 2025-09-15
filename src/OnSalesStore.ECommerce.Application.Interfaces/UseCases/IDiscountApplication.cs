using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.UseCases
{
    public interface IDiscountApplication
    {
        Task<Response<bool>> Add(DiscountDTO discountDTO, CancellationToken cancellationToken = default);

        Task<Response<bool>> Edit(DiscountDTO discountDTO, CancellationToken cancellationToken = default);

        Task<Response<bool>> Remove(int id, CancellationToken cancellationToken = default);

        Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default);

        Task<Response<IEnumerable<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default);

        Task<ResponsePagination<IEnumerable<DiscountDTO>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);

    }
}
