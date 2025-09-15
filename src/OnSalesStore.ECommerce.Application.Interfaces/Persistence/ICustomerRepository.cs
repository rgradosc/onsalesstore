using OnSalesStore.ECommerce.Domain.Entities;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface ICustomerRepository : IGenericRepository<Customer>, IPaginationRepository<Customer>
    {

    }
}
