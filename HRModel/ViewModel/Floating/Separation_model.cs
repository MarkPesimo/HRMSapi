using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Floating
{
    public class Separation_model
    {
        public class SeparationReason_model
        {
            public int Id { get; set; }
            public string Reason { get; set; }
        }

        public class EmployeeSeparationRecord_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }

            public string SeparationReason { get; set; }
            public int SeparationReasonId { get; set; }

            public DateTime SeparationDate { get; set; }
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

        public class Separation
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateInactive { get; set; }
            public int InactiveById { get; set; }
            public string InactiveBy { get; set; }
            public int SeparationId { get; set; }


            public int Mode { get; set; }
            public int LoginUserId { get; set; }
        }
    }
}
