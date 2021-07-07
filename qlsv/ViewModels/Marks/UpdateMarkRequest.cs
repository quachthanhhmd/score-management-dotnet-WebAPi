using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class UpdateMarkRequest
    {
        public string SubjectId { get; set; }
        public Guid? UserId { set; get; }
        public float? marks { set; get; }

    }
}
