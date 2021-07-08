using Microsoft.AspNetCore.Identity;
using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Users : IdentityUser<Guid>
    {
        public string LocalId { get; set; }    
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int Age { set; get; }
        public string PhotoPath { set; get; }
        public string Address { set; get; }
        public Genders Gender { set; get; }
        public Roles Role { set; get; }
        
    }
}
