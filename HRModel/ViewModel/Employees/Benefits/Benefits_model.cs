using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Benefits
{
    public class Benefits_model
    {
        public class BenefitsType_model
        {
            public int Id { get; set; }
            public string BenefitType { get; set; }
        }

        public class BenefitsClass_model
        {
            public int Id { get; set; }
            public string BenefitClass { get; set; }
        }

        public class BenefitsCategory_model
        {
            public int Id { get; set; }
            public string BenefitCategory { get; set; }
        }

        public class Adjustment_model
        {
            public int Id { get; set; }
            public string AdjustmentDescription { get; set; }
        }

        public class BenefitsRecord_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }

            public int BenefitTypeId { get; set; }
            public string BenefitType { get; set; }

            public int BenefitClassId { get; set; }
            public string BenefitClass { get; set; }

            public int BenefitCatId { get; set; }
            public string BenefitCategory { get; set; }

            public int AdjustmentId { get; set; }
            public string AdjustmentDescription { get; set; }

            public int ClientId { get; set; }
            public string ClientName { get; set; }

            public decimal Amount { get; set; }
            public DateTime EffectiveDate { get; set; }
            public DateTime StartDate { get; set; }
            public bool IsTaxable { get; set; }
            public bool IsBillable { get; set; }
            public bool Status { get; set; }
            public string Remarks { get; set; }

            public int UserCreatedId { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
            public int UserModifiedId { get; set; }
            public string ModifiedBy { get; set; }
            public string DateModified { get; set; }
            public int Mode { get; set; }
        }

        public class BenefitsMonitoring_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }

            public int ClientId { get; set; }
            public string ClientName { get; set; }

            public int BenefitTypeId { get; set; }
            public string BenefitType { get; set; }

            public int BenefitClassId { get; set; }
            public string BenefitClass { get; set; }

            public int BenefitCatId { get; set; }
            public string BenefitCategory { get; set; }

            public int AdjustmentId { get; set; }
            public string AdjustmentDescription { get; set; }            

            public decimal Amount { get; set; }
            public DateTime EffectiveDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Taxable { get; set; }
            public string Billable { get; set; }
            public string Remarks { get; set; }
        }
    }
}
