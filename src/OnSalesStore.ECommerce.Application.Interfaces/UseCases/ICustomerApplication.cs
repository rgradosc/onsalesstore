using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.UseCases
{
    public interface ICustomerApplication
    {
        #region Metodos sincronos

        Response<bool> Add(CustomerDTO customerDTO);

        Response<bool> Edit(CustomerDTO customerDTO);

        Response<bool> Remove(string customerId);

        Response<CustomerDTO> Get(string customerId);

        Response<IEnumerable<CustomerDTO>> GetAll();

        ResponsePagination<IEnumerable<CustomerDTO>> GetAllWithPagination(int pageNumber, int pageSize);

        #endregion

        #region Metodos asincronos

        Task<Response<bool>> AddAsync(CustomerDTO customerDTO);

        Task<Response<bool>> EditAsync(CustomerDTO customerDTO);

        Task<Response<bool>> RemoveAsync(string customerId);

        Task<Response<CustomerDTO>> GetAsync(string customerId);

        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();

        Task<ResponsePagination<IEnumerable<CustomerDTO>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);

        #endregion
    }
}
