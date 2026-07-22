using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees
{
    public class EmployeeMonitoringViewModel
    {
        public int RowNo { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Employer")]
        public string EmployerName { get; set; }

        public string Client { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string EmployeeType { get; set; }
        public string SourceType { get; set; }
        public string PayType { get; set; }

        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        public DateTime DateHired { get; set; }

        [Display(Name = "Encoded By")]
        public string UserEncoded { get; set; }

        [Display(Name = "Date Encoded")]
        public DateTime DateEncoded { get; set; }

        // Derived
        public string Initials => !string.IsNullOrEmpty(EmployeeName)
            ? string.Concat(EmployeeName.Replace(",", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Take(2).Select(s => s[0].ToString().ToUpper()))
            : "?";
    }
}
