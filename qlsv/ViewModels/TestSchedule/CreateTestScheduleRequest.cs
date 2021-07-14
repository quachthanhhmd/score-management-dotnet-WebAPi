using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.TestSchedule
{
    public class CreateTestScheduleRequest
    {

        [Required]
        [Display(Name = "Class Id")]
        public string ClassId { set; get; }

        [Display(Name = "Ngày thi")]
        public DateTime ExamTime { set; get; }
        [Display(Name = "Giờ thi")]
        public string ExamHour { set; get; }

        [Display(Name = "Tên giám thị")]
        public string SupervisorName { set; get; }
        [Display(Name = "Phòng thi")]
        public string RoomId { set; get; }
    }
}
