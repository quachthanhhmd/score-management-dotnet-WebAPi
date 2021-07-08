using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels
{
    public class AddClassRequest
    {
        [Display(Name = "ID của lớp học")]
        public string ClassId { set; get; }
        [Display(Name = "Tên lớp học")]
        [MaxLength(100, ErrorMessage = "Tên của lớp học phải dưới 100 ký tự.")]
        public string ClassName { get; set; }
        [Display(Name = "Số lượng học viên")]
        public int Capacity { get; set; }
        [Display(Name = "ID của giảng viên")]
        public string? TeacherId { get; set; }
        [Display(Name = "Mã phòng học")]
        public string RoomId { get; set; }
        [Display(Name = "Số tiết học")]
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
