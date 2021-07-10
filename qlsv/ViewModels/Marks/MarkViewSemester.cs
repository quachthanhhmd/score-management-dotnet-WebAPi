using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class MarkViewSemester
    {

        public string ClassTime { set; get; }
        public string ClassId { set; get; }
        public string ClassName { set; get; }
        public int Credit { set; get; }
        public float? Mark { set; get; }
    }
}
