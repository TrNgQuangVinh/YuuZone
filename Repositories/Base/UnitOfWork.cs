using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly YuuZoneDbContext _context;
        private IDbContextTransaction? _transaction = null;

        public UnitOfWork(YuuZoneDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
           _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if(_transaction!=null)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            if(_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }   
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            _context.Dispose();
            //Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /*protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }*/
    }
}
