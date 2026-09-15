using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Education
{
    public class EmployeeEducationModel
    {

    }

    public class SchoolList
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class SchoolLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class DegreeList
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
