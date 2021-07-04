using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace qlsv.Data.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}