using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Model.Dtos.User
{
    public class AddUserInput
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
    }
}
