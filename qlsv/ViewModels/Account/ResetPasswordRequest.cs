using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels.Account
{
    public class ResetPasswordRequest
    {

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password, ErrorMessage = "Password không đúng format")]
        public string Password { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu không khớp")]
        public string PasswordConfirm { set; get; }
    }
}
