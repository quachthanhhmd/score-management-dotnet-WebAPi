using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class MarkGPAView
    {
        public List<MarkView> ListMark { set; get; }
        public string StudentId { set; get; }
        public string StudentName { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public DateTime Dob { set; get; }
        public int totalCredit { set; get; }
        public float GPA { set; get; }
    }
}
