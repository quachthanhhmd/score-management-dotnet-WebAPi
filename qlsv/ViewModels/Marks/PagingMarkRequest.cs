using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Marks
{
    public class PagingMarkRequest: PagingBase
    {

        public Guid? UserID { set; get; }
        public string SubjectId { set; get; }
    }
}
