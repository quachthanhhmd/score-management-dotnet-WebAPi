using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.ClassRoom
{
    public class PagingClassRequest: PagingBase
    {
        [Display(Name = "Tòa nhà")]
        public string Building { set; get; }
        [Display(Name = "Số lượng bàn")]
        public int? Desks { set; get; }
        [Display(Name = "Số lượng ghế")]
        public int? Seats { set; get; }
    }
}
