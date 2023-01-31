using HalezService.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Model.Dtos.User
{
    public class LoginResult
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string NameSurname { get; set; }
        public string SecurityLevel { get; set; }
        public CustomerTypes CustomerType { get; set; }
    }
}
