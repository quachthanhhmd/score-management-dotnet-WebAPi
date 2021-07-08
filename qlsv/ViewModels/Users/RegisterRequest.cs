using Microsoft.AspNetCore.Http;
using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace qlsv.ViewModels
{
    public class RegisterRequest
    {
        [Display(Name = "Nhập username của bạn")]
        public string userName { get; set; }

        [Display(Name = "ID của bạn")]
        [MaxLength(20, ErrorMessage = "Mã số sinh viên/cán bộ phải dưới 20 số.")]
        public string LocalID { get; set; }
        [Display(Name = "Tên")]
        [MaxLength(100, ErrorMessage = "Tên người dùng phải dưới 100 ký tự.")]
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
        [DataType(DataType.Password, ErrorMessage ="Password không đúng format")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Giới tính")]
        public Genders Genders { get; set; }

        [Display(Name = "Tuổi")]
        public int Age { get; set; }

        [Display(Name = "Avatar" )]
        public IFormFile Photo { set; get; }
    }
}
