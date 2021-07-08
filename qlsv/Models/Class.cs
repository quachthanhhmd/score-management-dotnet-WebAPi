using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Class
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public int Capacity { get; set; }
        public int? NumberLessons { set; get; }
        public int? NumberCredits { set; get; }
        public int? Year { set; get; }
        public int? Semester { set; get; }
        public string TeacherId { get; set; }
        public string RoomId { get; set; }
        public string DepartmentId { get; set; }

    }
}
