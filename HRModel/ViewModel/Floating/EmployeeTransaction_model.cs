using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Floating
{
    public class EmployeeTransaction_model
    {
        public class TransactionLog
        {
            public int TranId { get; set; }
            public string TranAction { get; set; }
            public int UserId { get; set; }
            public DateTime DateValue { get; set; }
        }

        public class Separation
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
