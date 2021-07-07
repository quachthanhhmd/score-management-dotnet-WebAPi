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
        public string ClassName { get; set; }
        [Display(Name = "Số lượng học viên")]
        public int Capacity { get; set; }
        [Display(Name = "ID của giảng viên")]
        public Guid? TeacherId { get; set; }
        [Display(Name = "Mã phòng học")]
        public string RoomId { get; set; }
    }
}
