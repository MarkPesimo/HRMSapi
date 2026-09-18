using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Floating
{
    public class Replacement_model
    {
        public class EmployeeReplacement_vw_model
        {
            public int Id { get; set; }
            public DateTime ReplacementDate { get; set; }
            public string Remarks { get; set; }
        }

        public class EmployeeReplacement_model
        {
            public int Mode { get; set; }
            public int Id { get; set; }
            public int EmpId { get; set; }

            public DateTime ReplacementDate { get; set; }
            public string Remarks { get; set; }

            public int FloatingId { get; set; }
            public int SeparationId { get; set; }

            public int Userid { get; set; }
        }
    }
}
