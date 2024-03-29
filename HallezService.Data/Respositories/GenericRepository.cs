﻿using HallezService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HallezService.Data.Respositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(TEntity tEntity)
        {
            _dataContext.Set<TEntity>().Add(tEntity);
        }

        public void Add(IEnumerable<TEntity> tEntityList)
        {
            _dataContext.Set<TEntity>().AddRange(tEntityList);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dataContext.Set<TEntity>().Any(predicate);
        }

        public TEntity Get(int id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }

        public TEntity Get(long id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get()
        {
            return _dataContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dataContext.Set<TEntity>().Where(predicate);
        }

        public void Remove(TEntity tEntity)
        {
            _dataContext.Set<TEntity>().Remove(tEntity);
        }

        public void Remove(IEnumerable<TEntity> tEntityList)
        {
            _dataContext.Set<TEntity>().RemoveRange(tEntityList);
        }

        public void Remove(IQueryable<TEntity> tEntityQuery)
        {
            _dataContext.Set<TEntity>().RemoveRange(tEntityQuery);
        }
    }
}
