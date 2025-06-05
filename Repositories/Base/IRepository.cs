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
        void Create(T entity);
        Task<int> CreateAsync(T entity);
        Task Add(T entity);
        Task Add(IEnumerable<T> entities);

        // ========== UPDATE ==========
        void Update(T entity);
        Task<int> UpdateAsync(T entity);
        Task Update(IEnumerable<T> entities);

        // ========== DELETE ==========
        bool Remove(T entity);
        Task<bool> RemoveAsync(T? entity);
        void Remove(int id);
        void Remove(params T[] entities);
        void Remove(IEnumerable<T> entities);
    }

}
