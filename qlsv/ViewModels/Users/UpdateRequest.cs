using qlsv.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.ViewModels
{
    public class UpdateRequest
    {
        public string? Name { get; set; }
        public DateTime? Dob { get; set; }
        public int? Age { set; get; }
        public string? Address { get; set; }
        public Genders? Genders { get; set; }

    }
}
