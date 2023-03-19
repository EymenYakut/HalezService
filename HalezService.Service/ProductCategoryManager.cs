using HalezService.Entities;
using HalezService.Model.Dtos.ProductCategory;
using HalezService.Model.Dtos.User;
using HalezService.Model.Shared;
using HallezService.Data.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Service
{
    public class ProductCategoryManager
    {

        private readonly IUnitOfWork unitOfWork;

        public ProductCategoryManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ProductCategoryManager()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public void Insert(ProductCategoryInput productCategoryInput, int userId)
        {
            try
            {
                unitOfWork.BeginTransaction();
                ProductCategory productCategory = new()
                {
                   Name = productCategoryInput.Name,
                   Definition = productCategoryInput.Definition,
                   CreatedBy = userId,
                   CreatedDate = DateTime.Now,
                   IsDeleted=false
                };
                unitOfWork.ProductCategories.Add(productCategory);
                unitOfWork.SaveChanges();

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception(MyExceptionHandler.GetAllExceptionMessages(exception: ex));
            }
        }




    }
}
