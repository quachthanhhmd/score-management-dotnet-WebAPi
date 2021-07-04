using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class Marks
    {
        
        public string SubjectId { get; set; }
        public Guid UserId { set; get; }
        public float marks { set; get; }
        public Users Users { get; set; }
    }
}
