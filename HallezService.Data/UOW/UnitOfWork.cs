using HalezService.Entities;
using HallezService.Data.Context;
using HallezService.Data.Respositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallezService.Data.UOW
{
    public class UnitOfWork:IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DataContext _dataContext;



        public UnitOfWork()
        {
            _dataContext = new DataContext();
        }

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _dataContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        #region Private
        private IGenericRepository<User> _users { get; set; }
        private IGenericRepository<ProductCategory> _productCategories { get; set; }
        #endregion

        public IGenericRepository<User> Users => _users ?? new GenericRepository<User>(_dataContext);
        public IGenericRepository<ProductCategory> ProductCategories => _productCategories ?? new GenericRepository<ProductCategory>(_dataContext);


    }
}
