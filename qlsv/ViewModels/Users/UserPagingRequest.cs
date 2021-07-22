using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Users
{
    public class UserPagingRequest : PagingBase
    {
        public string Keyword { set; get; }
        public string RoleName { set; get; }

    }
}
