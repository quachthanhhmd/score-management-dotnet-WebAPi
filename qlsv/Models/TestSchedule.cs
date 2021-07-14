using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models
{
    public class TestSchedule
    {
        public int Id { set; get; }
        public string ClassId { set; get; }
        public DateTime ExamTime { set; get; }

        //Because Supervisor is able to unwork in the university
        public string SupervisorName { set; get; }
        public TimeSpan ExamHour { set; get; }

        public string RoomId { set; get; }
    }
}
