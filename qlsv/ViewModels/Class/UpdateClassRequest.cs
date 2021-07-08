using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Class
{
    public class UpdateClassRequest
    {
            
        [Display(Name = "Mã Lớp Học")]
        public string ClassId { get; set; }
        [Display(Name = "Tên lớp học")]
        public string ClassName { get; set; }
        [Display(Name = "Sức chứa của lớp học")]
        public int? Capacity { get; set; }
        [Display(Name = "Id của giáo viên")]
        public string? TeacherId { set; get; }
        [Display(Name = "ID của phòng học")]
        public string RoomId { set; get; }
        public int? NumberLessons { set; get; }
        [Display(Name = "Số chỉ")]
        public int? NumberCredits { set; get; }
        [Display(Name = "Năm tổ chức")]
        public int? Year { set; get; }
        [Display(Name = "Học Kì")]
        public int? Semester { set; get; }
        [Display(Name = "Mã bộ môn")]
        public string DepartmentId { set; get; }
    }
}
