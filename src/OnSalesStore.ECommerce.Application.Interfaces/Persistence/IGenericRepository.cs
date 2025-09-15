using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : class
    {

        #region Metodos sincronos

        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(string id);

        T Select(string id);

        IEnumerable<T> SelectAll();

        #endregion

        #region Metodos asincronos

        Task<bool> InsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(string id);

        Task<T> SelectAsync(string id);

        Task<IEnumerable<T>> SelectAllAsync();

        #endregion
    }
}
