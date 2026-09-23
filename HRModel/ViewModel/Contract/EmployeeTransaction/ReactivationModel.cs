using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Contract.EmployeeTransaction
{
    public class ReactivationModel
    {
        public class EmployeeReactivation_model
        {
            public int Id { get; set; }
            public int TransactionId { get; set; }
            public int SeparationId { get; set; }
            public int RequestedByUserid { get; set; }
            public string Reason { get; set; }
            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public class EmployeeReactivationMonitoring_model
        {
            public int Id { get; set; }
            public DateTime DateCreated { get; set; }
            public string ClientName { get; set; }
            public int EmpId { get; set; }
            public string EmployeeGUID { get; set; }
            public string EmployeeName { get; set; }
            public string RequestedBy { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateClosed { get; set; }
            public int TranId { get; set; }
        }
    }
}
