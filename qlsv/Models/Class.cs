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
        public Guid TeacherId { get; set; }
        public string RoomId { get; set; }
        public Users Users { get; set; }
    }
}
