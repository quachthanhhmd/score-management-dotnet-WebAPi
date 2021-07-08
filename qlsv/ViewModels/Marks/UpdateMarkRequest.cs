using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class UpdateMarkRequest
    {
        [Display(Name = "ID của môn học")]
        public string SubjectId { get; set; }
        [Display(Name = "ID của học sinh")]
        public string? UserId { set; get; }
        [Display(Name = "Điểm số")]
        [Range(0, 10)]
        public float? marks { set; get; }

    }
}
