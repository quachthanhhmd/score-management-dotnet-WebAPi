using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.ClassRoom
{
    public class PagingClassRequest: PagingBase
    {
        public string Building { set; get; }
        public int? Desks { set; get; }
        public int? Seats { set; get; }
    }
}
