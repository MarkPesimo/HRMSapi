using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Leave
{
    public class EmployeeLeaveBalanceMonitoring_model
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public string EntitledLeave { get; set; }
        public string EarnedPerMonth { get; set; }
        public string AvailableLeave { get; set; }
        public string UsedLeave { get; set; }
        public string Balance { get; set; }
        public string AddedBy { get; set; }
        public string DateAdded { get; set; }
        public string Status { get; set; }
        public string YearEntitled { get; set; }
        public int ClientId { get; set; }
        public int EmpId { get; set; }
        public string WhenCreditisEarned { get; set; }
        public string EmployeeGUID { get; set; }
    }

    public class LeaveBalanceFilter_model
    {
        public int YearEntitled { get; set; } = 2026; // Default to current year
        public bool ByClient { get; set; }
        public int ClientId { get; set; }
        public bool ByLeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public string Keyword { get; set; } = "";
        public int CompanyId { get; set; }
        public int EmpId { get; set; }
    }
}
