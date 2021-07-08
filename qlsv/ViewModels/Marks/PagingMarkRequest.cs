using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class PagingMarkRequest: PagingBase
    {
        [Display(Name = "Id của học sinh")]
        public string? UserID { set; get; }
        [Display(Name = "Id của lớp học")]
        public string SubjectId { set; get; }
    }
}
