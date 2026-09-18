using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Loan
{
    public class Loan_model
    {
        public class EmployeeLoanMonitoringModel
        {
            public int Id { get; set; }
            public string LoanGUID { get; set; }
            public string LoanDate { get; set; }
            public string LoanType { get; set; }
            public string ClientName   { get; set; }
            public string EmployeeName { get; set; }
            public string Remarks { get; set; }
            public string LoanAmount { get; set; }
            public string Balance { get; set; }
            public string Deducted { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
        }

        public class LoanFilter_model
        {
            public bool ByLoan { get; set; }
            public int LoanTypeId { get; set; }
            public bool ByDate { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public bool ByStatus { get; set; }
            public string Status { get; set; }
            public string Keyword { get; set; }
            public int CompanyId { get; set; }
        }

        public class EmployeeLoanModel
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public int LoanTypeId { get; set; }
            public DateTime LoanDate { get; set; }
            public DateTime LoanStartDate { get; set; }
            public decimal LoanAmount { get; set; }
            public decimal Balance { get; set; }
            public decimal DeductionAmount { get; set; }
            public string Remarks { get; set; }
            public bool LoanStatus { get; set; }
            public bool ForDeduction { get; set; }
            public int UserId { get; set; }
            public int Mode { get; set; }
        }
    }
}
