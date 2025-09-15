using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetCustomerQuery
{
    public sealed record GetCustomerQuery : IRequest<Response<CustomerDTO>>
    {
        public string CustomerId { get; set; }
    }
}
