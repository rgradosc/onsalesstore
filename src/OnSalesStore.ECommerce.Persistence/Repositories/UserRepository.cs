using Dapper;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Persistence.Contexts;
using System.Data;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            using (var connection = _context.CreateConnection())
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                p.Add("@PasswordHash", password);

                var user = await connection.QuerySingleAsync<User>("UsersGetByUserNameAndPassword", param: p, commandType: CommandType.StoredProcedure);

                return user;
            }
        }
    }
}
