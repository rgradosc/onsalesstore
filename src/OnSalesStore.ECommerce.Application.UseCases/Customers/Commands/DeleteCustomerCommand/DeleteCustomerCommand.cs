using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public sealed record DeleteCustomerCommand : IRequest<Response<bool>>
    {
        public string CustomerId { get; set; }
    }
}
