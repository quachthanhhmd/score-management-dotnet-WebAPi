using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.TestSchedule
{
    public class TestScheduleExportBase
    {

        public TestScheduleExportBase() { }
        public TestScheduleExportBase(TestScheduleExportBase data)
        {
            Title = data.Title;
            Classes = data.Classes;
            Signer = data.Signer;
        }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Classes will be applied")]
        public string Classes { get; set; }
        [Required]
        [Display(Name = "Signer Name")]
        public string Signer { get; set; }
    }
}
