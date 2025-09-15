using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Persistence.Contexts;
using OnSalesStore.ECommerce.Persistence.Mocks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Persistence.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DiscountRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Discount Select(string id)
        {
            return _applicationDbContext.Set<Discount>().AsNoTracking()
                .SingleOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Discount> SelectAll()
        {
            return _applicationDbContext.Set<Discount>().AsNoTracking().ToList();
        }

        public bool Insert(Discount entity)
        {
            _applicationDbContext.Add(entity);
            return true;
        }

        public bool Update(Discount entity)
        {
            var discount = _applicationDbContext.Set<Discount>().AsNoTracking()
                .SingleOrDefault(x => x.Id.Equals(entity.Id));
            if (discount == null)
            {
                return false;
            }
            discount.Name = entity.Name;
            discount.Description = entity.Description;
            discount.Percent = entity.Percent;
            discount.Status = entity.Status;
            _applicationDbContext.Update(discount);
            return true;
        }

        public bool Delete(string id)
        {
            var discount = _applicationDbContext.Set<Discount>()
                .AsNoTracking().SingleOrDefault(x => x.Id.ToString().Equals(id));
            if (discount == null)
            {
                return false;
            }
            _applicationDbContext.Remove(discount);
            return true;
        }

        public async Task<IEnumerable<Discount>> SelectAllAsync()
        {
            return await _applicationDbContext.Set<Discount>().AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Discount>> SelectAllAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Discount>().AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Discount> SelectAsync(int id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Discount>().AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<Discount> SelectAsync(string id)
        {
            return await _applicationDbContext.Set<Discount>().AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> InsertAsync(Discount entity)
        {
            _applicationDbContext.Add(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Discount entity)
        {
            var discount = await _applicationDbContext.Set<Discount>().AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
            if (discount == null)
            {
                return await Task.FromResult(false);
            }
            discount.Name = entity.Name;
            discount.Description = entity.Description;
            discount.Percent = entity.Percent;
            discount.Status = entity.Status;
            _applicationDbContext.Update(discount);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var discount = await _applicationDbContext.Set<Discount>()
                .AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString().Equals(id));
            if (discount == null)
            {
                return await Task.FromResult(false);
            }
            _applicationDbContext.Remove(discount);
            return await Task.FromResult(true);

        }

        public IEnumerable<Discount> SelectAllWithPagination(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int Count()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Discount>> SelectAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var faker = new DiscountGetAllWithPaginationAsyncBogusConfig();
            var result = await Task.Run(() => faker.Generate(1000));

            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() => 1000);
        }
    }
}
