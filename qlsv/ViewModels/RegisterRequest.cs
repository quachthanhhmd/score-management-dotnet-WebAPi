using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace qlsv.ViewModels
{
    public class RegisterRequest
    {
        [Display(Name = "ID của bạn")]
        public string userName { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Hòm thư")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Giới tính")]
        public Genders Genders { get; set; }

        [Display(Name = "Tuổi")]
        public int Age { get; set; }
    }
}
