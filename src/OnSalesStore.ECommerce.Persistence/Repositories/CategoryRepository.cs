using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Persistence.Contexts;
using OnSalesStore.ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace OnSalesStore.ECommerce.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> SelectAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT CategoryId, CategoryName, Description, Picture FROM Categories";
                var categories = await connection.QueryAsync<Category>(query, commandType: CommandType.Text);
                return categories;
            }
        }
    }
}
