using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace OnSalesStore.ECommerce.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;

        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public bool Delete(string customerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customerId);

                var result = connection.Execute("CustomersDelete", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customerId);

                var result = await connection.ExecuteAsync("CustomersDelete", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public Customer Select(string customerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customerId);

                var customer = connection.QuerySingle<Customer>("CustomersGetByID", param: p, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public async Task<Customer> SelectAsync(string customerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customerId);

                var customer = await connection.QuerySingleAsync<Customer>("CustomersGetByID", param: p, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public IEnumerable<Customer> SelectAll()
        {
            using (var connection = _context.CreateConnection())
            {

                var customers = connection.Query<Customer>("CustomersList", commandType: CommandType.StoredProcedure);

                return customers;
            }
        }

        public async Task<IEnumerable<Customer>> SelectAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var customers = await connection.QueryAsync<Customer>("CustomersList", commandType: CommandType.StoredProcedure);

                return customers;
            }
        }

        public IEnumerable<Customer> SelectAllWithPagination(int pageNumber, int pageSize)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@PageNumber", pageNumber);
                p.Add("@PageSize", pageSize);
                var customer = connection.Query<Customer>("CustomersListWithPagination", param: p, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public async Task<IEnumerable<Customer>> SelectAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@PageNumber", pageNumber);
                p.Add("@PageSize", pageSize);
                var customer = await connection.QueryAsync<Customer>("CustomersListWithPagination", param: p, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public bool Insert(Customer customer)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customer.CustomerId);
                p.Add("@CompanyName", customer.CompanyName);
                p.Add("@ContactName", customer.ContactName);
                p.Add("@ContactTitle", customer.ContactTitle);
                p.Add("@Address", customer.Address);
                p.Add("@City", customer.City);
                p.Add("@Region", customer.Region);
                p.Add("@PostalCode", customer.PostalCode);
                p.Add("@Country", customer.Country);
                p.Add("@Phone", customer.Phone);
                p.Add("@Fax", customer.Fax);

                var result = connection.Execute("CustomersInsert", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customer.CustomerId);
                p.Add("@CompanyName", customer.CompanyName);
                p.Add("@ContactName", customer.ContactName);
                p.Add("@ContactTitle", customer.ContactTitle);
                p.Add("@Address", customer.Address);
                p.Add("@City", customer.City);
                p.Add("@Region", customer.Region);
                p.Add("@PostalCode", customer.PostalCode);
                p.Add("@Country", customer.Country);
                p.Add("@Phone", customer.Phone);
                p.Add("@Fax", customer.Fax);

                var result = await connection.ExecuteAsync("CustomersInsert", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public bool Update(Customer customer)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customer.CustomerId);
                p.Add("@CompanyName", customer.CompanyName);
                p.Add("@ContactName", customer.ContactName);
                p.Add("@ContactTitle", customer.ContactTitle);
                p.Add("@Address", customer.Address);
                p.Add("@City", customer.City);
                p.Add("@Region", customer.Region);
                p.Add("@PostalCode", customer.PostalCode);
                p.Add("@Country", customer.Country);
                p.Add("@Phone", customer.Phone);
                p.Add("@Fax", customer.Fax);

                var result = connection.Execute("CustomersUpdate", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@CustomerID", customer.CustomerId);
                p.Add("@CompanyName", customer.CompanyName);
                p.Add("@ContactName", customer.ContactName);
                p.Add("@ContactTitle", customer.ContactTitle);
                p.Add("@Address", customer.Address);
                p.Add("@City", customer.City);
                p.Add("@Region", customer.Region);
                p.Add("@PostalCode", customer.PostalCode);
                p.Add("@Country", customer.Country);
                p.Add("@Phone", customer.Phone);
                p.Add("@Fax", customer.Fax);

                var result = await connection.ExecuteAsync("CustomersUpdate", param: p, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public int Count()
        {
            using (var connection = _context.CreateConnection())
            {
                string query = "SELECT COUNT(1) FROM Customers;";
                return connection.ExecuteScalar<int>(query, commandType: CommandType.Text);
            }
        }

        public async Task<int> CountAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                string query = "SELECT COUNT(1) FROM Customers;";
                return await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            }
        }
    }
}
