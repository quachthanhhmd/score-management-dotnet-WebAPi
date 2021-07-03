using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Subjects
    {
        public string SubjectId { get; set; }
        public string Name { set; get; }
        public int NumberLessons { set; get; }
        public int NumberCredits { set; get; }
        public int Year { set; get; }
        public int Semester { set; get; }
    }
}
