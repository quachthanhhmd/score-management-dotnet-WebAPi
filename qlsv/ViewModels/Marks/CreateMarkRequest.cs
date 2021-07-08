using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels
{
    public class CreateMarkRequest
    {
        [Required]
        [Display(Name = "Id của môn học")]
        public string SubjectId { get; set; }

        [Required]
        [Display(Name = "Id của học sinh")]
        public string StudentID { set; get; }

        [Display(Name = "Điểm số")]
        [Range(0, 10)]
        public float? marks { set; get; }
    }
}
