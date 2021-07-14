using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.TestSchedule
{
    public class TestScheduleClassView
    {
        public string ClassId { set; get; }
        public string ClassName { set; get; }
        public DateTime TestTime { set; get; }
        public string TestHour { set; get; }
        public string RoomId { set; get; }
        public string SupervisorName { set; get; }
    }
}
