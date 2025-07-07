using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constant
{
    public class StringConstants
    {
        public const string PHONE_NUMBER_REGEX = @"^0[7-9]{1}\d{9}$";
        
    }
    public static class RoleConstant
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Principal = "Principal";
        public const string Accountant = "Accountant";
        public const string FormMaster = "FormMaster";
        public const string HeadMaster = "HeadMaster";
        public const string Teacher = "Teacher";
        public const string Student = "Student";
        public const string Parent = "Parent";
        public const string ExamOfficer = "ExamOfficer";
    }
}
