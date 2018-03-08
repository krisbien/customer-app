using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(int id, TEntity entity);
        Task DeleteAsync(int id);
    }
}
