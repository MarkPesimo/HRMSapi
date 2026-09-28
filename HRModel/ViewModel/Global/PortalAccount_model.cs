using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class PortalAccount_model
    {
        public class NotYetRegisteredEmployee_model
        {
            public int EmpId { get; set; }
            public string EmpNo { get; set; }
            public string EmployeeName { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string Middlename { get; set; }
            public string EmailAddress { get; set; }
        }

        public class RegisterPortalAccount_model
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string HashPassword { get; set; }
            public int EmpId { get; set; }
            public string EmailAddress { get; set; }
            public int UserId { get; set; }
        }
    }
}
