using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Transversal.Common;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.UseCases
{
    public interface IUserApplication
    {
        Task<Response<UserDTO>> Authenticate(AuthDTO userAuth);
    }
}
