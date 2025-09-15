using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Transversal.Common;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerApplication> _logger;

        public CustomerApplication(
            IUnitOfWork unitOfWork, IMapper mapper,ILogger<CustomerApplication> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Response<CustomerDTO> Get(string customerId)
        {
            var response = new Response<CustomerDTO>();

            var customer = _unitOfWork.Customers.Select(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }

            return response;
        }

        public Response<IEnumerable<CustomerDTO>> GetAll()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();

            var customers = _unitOfWork.Customers.SelectAll();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public Response<bool> Add(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = _unitOfWork.Customers.Insert(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro exitoso";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public Response<bool> Edit(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = _unitOfWork.Customers.Update(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualizacíón exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public Response<bool> Remove(string customerId)
        {
            var response = new Response<bool>();

            response.Data = _unitOfWork.Customers.Delete(customerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public async Task<Response<CustomerDTO>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDTO>();

            var customer = await _unitOfWork.Customers.SelectAsync(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();

            var customers = await _unitOfWork.Customers.SelectAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public async Task<Response<bool>> AddAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = await _unitOfWork.Customers.InsertAsync(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro exitoso";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public async Task<Response<bool>> EditAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = await _unitOfWork.Customers.UpdateAsync(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualizacíón exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public async Task<Response<bool>> RemoveAsync(string customerId)
        {
            var response = new Response<bool>();

            response.Data = await _unitOfWork.Customers.DeleteAsync(customerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
                _logger.LogInformation(response.Message);
            }

            return response;
        }

        public ResponsePagination<IEnumerable<CustomerDTO>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

            var count = _unitOfWork.Customers.Count();
            var customers = _unitOfWork.Customers.SelectAllWithPagination(pageNumber, pageSize);
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Consulta paginada correctamente!";
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<CustomerDTO>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

            var count = await _unitOfWork.Customers.CountAsync();
            var customers = await _unitOfWork.Customers.SelectAllWithPaginationAsync(pageNumber, pageSize);
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Consulta paginada correctamente!";
            }

            return response;
        }
    }
}
