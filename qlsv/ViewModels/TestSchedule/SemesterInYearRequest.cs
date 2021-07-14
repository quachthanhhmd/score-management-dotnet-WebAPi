using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.TestSchedule
{
    public class SemesterInYearRequest
    {
        [Required]
        [Display(Name = "Năm học")]
        public int Year { set; get; }
        [Required]
        [Display(Name = "Học kỳ")]
        public int Semester { set; get; }
       
    }
}
