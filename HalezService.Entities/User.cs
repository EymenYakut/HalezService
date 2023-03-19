using HalezService.Model.Enums;
using HalezService.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Entities
{
    public class User : EntityBase, IEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Gsm { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public UserTypes UserType { get; set; }
    }
}
