using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Departments
{
    public class CreateDepartmentRequest
    {

        [Display(Name = "Id của khoa")]
        public string DepartmentId { set; get; }
        [Display(Name = "Tên khoa")]
        public string Name { set; get; }
        [Display(Name = "Id của giáo viên")]
        public Guid? leaderId { set; get; }

    }
}
