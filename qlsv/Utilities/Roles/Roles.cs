using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Utilities.Roles
{
    public struct Roles
    {

        public const string Admin = "Admin";
        public const string Student = "Student";
        public const string Teacher = "Teacher";
        public const string All = "Admin, Teacher, Student";
        public const string Manager = "Admin, Teacher";
    }
}
