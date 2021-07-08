using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Departments
{
    public class PagingDepartmentRequest : PagingBase
    {
        [Display(Name = "Nhập tên trưởng khoa")]
        public string LeaderName { get; set; }
    }
}
