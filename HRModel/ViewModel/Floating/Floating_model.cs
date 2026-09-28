using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Floating
{
    public class Floating_model
    {
        public class FloatingReason_model
        {
            public int Id { get; set; }
            public string Reason { get; set; }
        }

        public class EmployeeFloatingRecord_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }

            public string FloatingReason { get; set; }
            public int FloatingReasonId { get; set; }

            public bool IsFloating { get; set; }
            public DateTime FloatingDate { get; set; }

            public bool WithSeparationPay { get; set; }
            public string Remarks { get; set; }

            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
            public int Mode { get; set; }

            public int ReplacementId { get; set; }
            public DateTime ReplacementDate { get; set; }
            public string ReplacementRemarks { get; set; }

            public int TransactionId { get; set; }              //contract id
            public bool ContractStatus { get; set; }
        }

        public class Floating
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public DateTime? DateResignationSubmitted { get; set; }
            public DateTime? DateSeparated { get; set; }
            public int SeparatedById { get; set; }
            public string SeparatedBy { get; set; }
            public int FloatingId { get; set; }

            public int Mode { get; set; }
            public int LoginUserId { get; set; }
        }
    }
}
