using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.ClassRoom
{
    public class CreateClassRoomRequest
    {
        [Display(Name = "ID của phòng học")]
        public string RoomID { set; get; }

        [Display(Name = "Số lượng chồ ngồi của phòng học")]
        public int? Seats { get; set; }
        [Display(Name = "Số lượng bàn")]
        public int? Desks { set; get; }

        [Display(Name = "Tòa nhà")]
        public string Building { set; get; }

    }
}
