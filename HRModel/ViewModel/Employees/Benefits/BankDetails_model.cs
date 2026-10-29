using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Benefits
{
    public class BankDetails_model
    {
        public class BankDetailsUpload_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }
            public int ClientId { get; set; } 
            public string ClientName { get; set; }
            public int BankId { get; set; } 
            public string BankName { get; set; } 
            public string CardNo { get; set; } 
            public string AccountNo { get; set; } 
            public DateTime CardValidityDate { get; set; } 
            public string ErrorMessage { get; set; }  
            public int UserId { get; set; }
        }
    }
}
