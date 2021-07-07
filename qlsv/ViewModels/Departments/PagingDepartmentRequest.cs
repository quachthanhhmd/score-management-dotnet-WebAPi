using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Departments
{
    public class PagingDepartmentRequest : PagingBase
    {
        public string LeaderName { get; set; }
    }
}
