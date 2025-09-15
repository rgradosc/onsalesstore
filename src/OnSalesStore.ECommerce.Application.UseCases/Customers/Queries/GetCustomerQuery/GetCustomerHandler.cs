using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetCustomerQuery
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<CustomerDTO>();

            var customer = await _unitOfWork.Customers.SelectAsync(request.CustomerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }

            return response;
        }
    }
}
