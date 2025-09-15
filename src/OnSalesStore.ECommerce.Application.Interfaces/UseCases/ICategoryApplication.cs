using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.UseCases
{
    public interface ICategoryApplication
    {
        Task<Response<IEnumerable<CategoryDTO>>> GetAll();
    }
}
