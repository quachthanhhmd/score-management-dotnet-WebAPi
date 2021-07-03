using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Users
    {
        public string Id { get; set; }
        public string Password { set; get; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int Age { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public Genders Gender { set; get; }
        public Roles Role { set; get; }
        
    }
}
