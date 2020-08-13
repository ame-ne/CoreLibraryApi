using CoreLibraryApi.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(string[] includes);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(int id, string[] includes);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<int> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        IDbTransaction BeginTransaction();

    }
}
