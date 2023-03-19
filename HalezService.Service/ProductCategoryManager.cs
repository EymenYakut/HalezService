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


    }
}
