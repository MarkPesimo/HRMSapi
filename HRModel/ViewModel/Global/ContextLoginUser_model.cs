using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class ContextLoginUser_model : ICustomPrincipal
    {
        public int UserId { get; set; }
        public int CandidateId { get; set; }
        public string Username { get; set; }

        public string Usertype { get; set; }

        public string EmailAddress { get; set; }

        public int AppModuleId { get; set; }

        public string EmployeeName { get; set; }
        public string RestDay { get; set; }
        public string Password { get; set; }
        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyLogo { get; set; }

        public string PayrollMenuVisibility { get; set; }
        public string DocumentMenuVisibility { get; set; }
        public string HelpdeskMenuVisibility { get; set; }
        public string OvertimeMenuVisibility { get; set; }
        public string LeaveMenuVisibility { get; set; }
        public string AttendanceMenuVisibility { get; set; }

        public int CountryId { get; set; }

        public string EmployerName { get; set; }

        public IIdentity Identity { get; private set; }
        public ContextLoginUser_model(string name)
        {
            this.Identity = new GenericIdentity(name, "Custom");
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
