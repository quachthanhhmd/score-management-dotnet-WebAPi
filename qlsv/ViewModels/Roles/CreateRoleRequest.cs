using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Roles
{
    public class CreateRoleRequest
    {
        public string RoleName { set; get; }    
        public string Description { set; get; }
    }
}
