using AutoMapper;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Domain.Specification;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var customer = _mapper.Map<Customer>(request);

            var countryInBlackListSpec = new CountryInBlackListSpecification();
            if (!countryInBlackListSpec.IsSatisfiedBy(customer))
            {
                response.IsSuccess = false;
                response.Message = $"Los clientes del país {customer.Country} no se pueden registrar porque se encuentra en lista negra.";
                return response;
            }

            response.Data = await _unitOfWork.Customers.InsertAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro exitoso";
            }

            return response;
        }
    }
}
