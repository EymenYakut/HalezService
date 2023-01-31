using HallezService.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallezService.Data.UOW
{
    public class UnitOfWork
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
        //private IGenericRepository<Customer> _customers { get; set; }
        #endregion

        //public IGenericRepository<Customer> Customers => _customers ?? new GenericRepository<Customer>(_dataContext);

    }
}
