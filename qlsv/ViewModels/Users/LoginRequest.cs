using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels
{
    public class LoginRequest
    {
        [Display(Name = "Nhập UserName")]
        public string UserName { get; set; }

        [Display(Name = "Nhâp password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
