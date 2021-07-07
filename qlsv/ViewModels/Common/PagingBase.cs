using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Common
{
    public class PagingBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
