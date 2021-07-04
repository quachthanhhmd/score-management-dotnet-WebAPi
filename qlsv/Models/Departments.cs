using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Departments
    {
        public string DepartmentId { set; get; }
        public string Name { set; get; }
        public Guid LeaderId { set; get; }

        public Users Users { set; get; }
    }
}
