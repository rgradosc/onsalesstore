using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Collections.Generic;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationQuery
{
    public sealed record GetAllWithPaginationQuery : IRequest<ResponsePagination<IEnumerable<CustomerDTO>>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
