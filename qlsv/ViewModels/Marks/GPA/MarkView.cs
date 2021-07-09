using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class MarkView
    {
        public string ClassName { set; get; }
        public string ClassId { set; get; }
        public int NumberCredit { set; get; }
        public int Year { set; get; }
        public int Semester { set; get; }
        public float Mark { set; get; }
    }
}
