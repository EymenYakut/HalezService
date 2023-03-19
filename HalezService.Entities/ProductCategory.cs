using HalezService.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Entities
{
    public class ProductCategory : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Definition { get; set; }
    }
}
