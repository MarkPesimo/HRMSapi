using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public interface ICustomPrincipal : IPrincipal
    {
        int UserId { get; set; }

        string Username { get; set; }

        string Usertype { get; set; }

        string EmailAddress { get; set; }

        int AppModuleId { get; set; }

        string EmployeeName { get; set; }

        string Password { get; set; }

        string EmployerName { get; set; }
    }
}
