using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly YuuZoneDbContext _dbContext;
        public DbSet<T> Entities { get; }

        public Repository(YuuZoneDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<T>();
        }

        // ========== READ ==========
        public List<T> GetAll()
        {
            return Entities.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public T? GetById(int id)
        {
            var entity = Entities.Find(id);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await Entities.FindAsync(id);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public T? GetById(string code)
        {
            var entity = Entities.Find(code);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> GetByIdAsync(string code)
        {
            var entity = await Entities.FindAsync(code);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public T? GetById(Guid code)
        {
            var entity = Entities.Find(code);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid code)
        {
            var entity = await Entities.FindAsync(code);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        // ========== CREATE ==========
        public int Create(T entity)
        {
            Entities.Add(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            Entities.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public int Create(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        public async Task<int> CreateAsync(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            return await _dbContext.SaveChangesAsync();
        }

        // ========== UPDATE ==========
        public int Update(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsyncReturnItem(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        // ========== DELETE ==========
        public bool Remove(T entity)
        {
            Entities.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T? entity)
        {
            if (entity == null) return false;
            Entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
