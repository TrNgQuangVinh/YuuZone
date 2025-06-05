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
        private readonly YuuZoneDbContext _dbContext;
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
        public void Create(T entity)
        {
            Entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            Entities.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Add(T entity)
        {
            Entities.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        // ========== UPDATE ==========
        public void Update(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<T> entities)
        {
            Entities.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
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

        public void Remove(int id)
        {
            var entity = GetById(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Entities.Remove(entity);
        }

        public void Remove(params T[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            Entities.RemoveRange(entities);
        }

        public void Remove(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            Entities.RemoveRange(entities);
        }
    }


}
