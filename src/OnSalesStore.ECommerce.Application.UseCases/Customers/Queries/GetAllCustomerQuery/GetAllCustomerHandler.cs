using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, Response<IEnumerable<CustomerDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CustomerDTO>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<CustomerDTO>>();

            var customers = await _unitOfWork.Customers.SelectAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }

            return response;
        }
    }
}
