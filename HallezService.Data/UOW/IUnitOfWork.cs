using HalezService.Entities;
using HallezService.Data.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallezService.Data.UOW
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<ProductCategory> ProductCategories { get; }


        int SaveChanges();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
