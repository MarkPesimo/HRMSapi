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

        public class PortalUser_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string Username { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public string Password { get; set; }
            public string EmailAddress { get; set; }
            public bool Status { get; set; }
            public string UserStatus { get; set; }
            public string DateRegistered { get; set; }

            public int UserId { get; set; }
            public int Mode { get; set; }
        }


        public class PortalUser_vw__model
        {
            public int RecountCount { get; set; }
            public List<PortalUser_model> PortalUsers { get; set; }
        }


        public class ClientPortalAccess_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int ModuleId { get; set; }
            public string ModuleName { get; set; }
            public int UserId { get; set; }
            public string ProvidedBy { get; set; }
            public string DateProvided { get; set; }
            public int Mode { get; set; }
        }
        //public class PortalUsers_vw_list
        //{
        //    public int Id { get; set; }
        //    //public int EmpId { get; set; }
        //    public string ClientName { get; set; }
        //    public string EmployeeName { get; set; }
        //    public string Username { get; set; }
        //    public string EmailAddress { get; set; }            
        //    public string UserStatus { get; set; }
        //    public string DateRegistered { get; set; }
        //}
    }
}
