using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class ClassRoom
    {
        public string RoomId { set; get; }
        public int? Seats { get; set; }
        public int? Desks { get; set;}
        public string Building { set; get; }
    }
}
