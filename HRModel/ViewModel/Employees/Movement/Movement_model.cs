using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Movement
{
    public class Movement_model
    {
        public class MovementCategory_model
        {
            public int Id { get; set; }
            public string Category { get; set; }
        }

        public class MovementRecord_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }

            public int CategoryId { get; set; }
            public string Category { get; set; }

            public int ClientIdFrom { get; set; }
            public string ClientFrom { get; set; }
            public int ClientIdTo { get; set; }
            public string ClientTo { get; set; }

            public int BranchIdFrom { get; set; }
            public string BranchFrom { get; set; }
            public int BranchIdTo { get; set; }
            public string BranchTo { get; set; }

            public int DepartmentIdFrom { get; set; }
            public string DepartmentFrom { get; set; }
            public int DepartmentIdTo { get; set; }
            public string DepartmentTo { get; set; }

            public int EmployeeTypeIdFrom { get; set; }
            public string EmployeeTypeFrom { get; set; }
            public int EmployeeTypeIdTo { get; set; }
            public string EmployeeTypeTo { get; set; }

            public string PositionFrom { get; set; }
            public string PositionTo { get; set; }

            public int EmployeeRankIdFrom { get; set; }
            public string EmployeeRankFrom { get; set; }
            public int EmployeeRankIdTo { get; set; }
            public string EmployeeRankTo { get; set; }

            public string PayTypeFrom { get; set; }
            public string PayTypeTo { get; set; }
            public decimal MonthlySalaryFrom { get; set; }
            public decimal MonthlySalaryTo { get; set; }
            public decimal AllowanceFrom { get; set; }
            public decimal AllowanceTo { get; set; }
            public DateTime EffectiveDateFrom { get; set; }
            public DateTime EffectiveDateTo { get; set; }
            public DateTime SalaryChangeDate { get; set; }
            public int UserCreatedId { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
            public int UserModifiedId { get; set; }
            public string ModifiedBy { get; set; }
            public string DateModified { get; set; }
            public bool ProcessStatus { get; set; }
            public string ProcessDate { get; set; }
            public int Mode { get; set; }
        }

        public class MovementMonitoring_model
        { 
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }
            public string ClientName { get; set; }
            public string Department { get; set; }
            public string Branch { get; set; }
            public string Position { get; set; }
            public string PayType { get; set; }
            public DateTime EffectiveDate { get; set; }
            public DateTime SalaryChangeDate { get; set; } 
        }
    }
}
