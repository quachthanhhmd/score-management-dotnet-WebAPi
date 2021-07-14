using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.TestSchedule
{
    public class TestScheduleView<T> : TestScheduleExportBase
    {

        public List<T> ScheduleTime { get; set; }


        public TestScheduleView(TestScheduleExportBase Data) : base(Data){
            
        }
        
        
    }
}
