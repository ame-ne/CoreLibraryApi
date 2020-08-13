using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreLibraryApi.Infrastructure
{
    public class DbTransaction : IDbTransaction
    {
        private IDbContextTransaction _transaction;

        public DbTransaction(ApplicationDbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
