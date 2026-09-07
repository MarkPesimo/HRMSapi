using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees
{
    public class PersonalInfo
    {

    }

    public class EmployeeProfile
    {
        public string GUid { get; set; }
        public PersonalInfo Personal { get; set; }
    }
}
