using Microsoft.AspNetCore.Http;
using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels
{
    public class UpdateRequest
    {
        
       
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

      
        [Display(Name = "Giới tính")]
        public Genders Genders { get; set; }

        [Display(Name = "Tuổi")]
        public int? Age { get; set; }

        [Display(Name = "Avatar")]
        public IFormFile? Photo { set; get; }

    }
}
