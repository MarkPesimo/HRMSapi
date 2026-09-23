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


    }
}
