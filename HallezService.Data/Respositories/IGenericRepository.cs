using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HallezService.Data.Respositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity tEntity);
        void Add(IEnumerable<TEntity> tEntityList);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        TEntity Get(long id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity tEntity);
        void Remove(IEnumerable<TEntity> tEntityList);
        void Remove(IQueryable<TEntity> tEntityQuery);
    }
}
