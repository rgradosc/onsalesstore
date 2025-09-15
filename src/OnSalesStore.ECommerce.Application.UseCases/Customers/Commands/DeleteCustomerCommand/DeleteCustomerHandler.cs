using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            response.Data = await _unitOfWork.Customers.DeleteAsync(request.CustomerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
            }

            return response;
        }
    }
}
