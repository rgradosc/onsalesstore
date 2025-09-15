using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Collections.Generic;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public sealed record GetAllCustomerQuery : IRequest<Response<IEnumerable<CustomerDTO>>>
    {
    }
}
