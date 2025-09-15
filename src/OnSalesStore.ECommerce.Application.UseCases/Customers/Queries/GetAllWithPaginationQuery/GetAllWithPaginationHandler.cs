using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationQuery
{
    public class GetAllWithPaginationHandler : IRequestHandler<GetAllWithPaginationQuery, ResponsePagination<IEnumerable<CustomerDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWithPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePagination<IEnumerable<CustomerDTO>>> Handle(GetAllWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

            var count = await _unitOfWork.Customers.CountAsync();
            var customers = await _unitOfWork.Customers.SelectAllWithPaginationAsync(request.PageNumber, request.PageSize);
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            if (response.Data != null)
            {
                response.PageNumber = request.PageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (decimal)request.PageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Consulta paginada correctamente!";
            }

            return response;
        }
    }
}
