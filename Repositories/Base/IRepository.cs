using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        // ========== READ ==========
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);

        T? GetById(string code);
        Task<T?> GetByIdAsync(string code);

        T? GetById(Guid code);
        Task<T?> GetByIdAsync(Guid code);

        // ========== CREATE ==========
        int Create(T entity);
        Task<int> CreateAsync(T entity);

        int Create(IEnumerable<T> entities);
        Task<int> CreateAsync(IEnumerable<T> entities);

        // ========== UPDATE ==========
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        Task<T> UpdateAsyncReturnItem(T entity);

        // ========== DELETE ==========
        bool Remove(T entity);
        Task<bool> RemoveAsync(T? entity);
    }

}
