using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Persistence.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ICustomerRepository customers, IUserRepository users,
            ICategoryRepository categories, IDiscountRepository discounts, ApplicationDbContext applicationDbContext)
        {
            Customers = customers;
            Users = users;
            Categories = categories;
            Discounts = discounts;
            _applicationDbContext = applicationDbContext;
        }

        public ICustomerRepository Customers { get; }

        public IUserRepository Users { get; }

        public ICategoryRepository Categories { get; }

        public IDiscountRepository Discounts { get; }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
