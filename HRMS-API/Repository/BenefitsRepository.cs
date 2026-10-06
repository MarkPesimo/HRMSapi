using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Employees.Benefits.Benefits_model;

namespace HRMS_API.Repository
{
    public class BenefitsRepository
    {
        private apwdbEntities _conn { get; set; }
        private BenefitsRepository _benefitsrepository { get; set; }

        public BenefitsRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_benefitsrepository == null) { _benefitsrepository = new BenefitsRepository(); }
        }

        public List<BenefitsType_model> GetBenefitsType()
        {
            return (from d in _conn.Employee_Benefits_Type
                    orderby d.benefits_type
                    select d).AsEnumerable()
               .Select(x => new BenefitsType_model()
               {
                   Id           = int.Parse(x.id.ToString()),
                   BenefitType  = x.benefits_type
               }).ToList();
        }

        public List<BenefitsClass_model> GetBenefitsClass()
        {
            List<BenefitsClass_model> _obj = new List<BenefitsClass_model>();
            BenefitsClass_model _benefits = new BenefitsClass_model();

            _benefits.Id = 0;
            _benefits.BenefitClass = "Fixed";
            _obj.Add(_benefits);

            _benefits = new BenefitsClass_model();
            _benefits.Id = 1;
            _benefits.BenefitClass = "Variable";
            _obj.Add(_benefits);

            _benefits = new BenefitsClass_model();
            _benefits.Id = 2;
            _benefits.BenefitClass = "NA";

            return _obj;
        }

        public List<BenefitsCategory_model> GetBenefitsCategory()
        {
            List<BenefitsCategory_model> _obj = new List<BenefitsCategory_model>();
            BenefitsCategory_model _benefits = new BenefitsCategory_model();

            _benefits.Id = 0;
            _benefits.BenefitCategory = "Cash";
            _obj.Add(_benefits);

            _benefits = new BenefitsCategory_model();
            _benefits.Id = 1;
            _benefits.BenefitCategory = "Non-Cash";
            _obj.Add(_benefits);

            _benefits = new BenefitsCategory_model();
            _benefits.Id = 2;
            _benefits.BenefitCategory = "NA";

            return _obj;
        }

        public List<BenefitsMonitoring_model> GetBenefitsMonitoring(string Keyword, bool ByClient, int ClientID, bool ByDate, DateTime DateFrom, DateTime DateTo, int PageNo, int PageSize, int CompanyID)
        {
            if (Keyword == "NULL") { Keyword = ""; }
            return (from d in _conn.USP_H_GET_EMPLOYEE_BENEFITS_MONITORING(Keyword, ByClient, ClientID, ByDate, DateFrom, DateTo, PageNo, PageSize, CompanyID)
                    select d).AsEnumerable()
                  .Select(x => new BenefitsMonitoring_model()
                  {
                      Id                    = int.Parse(x.id.ToString()),
                      EmpId                 = int.Parse(x.emp_id.ToString()),
                      EmployeeName          = x.EmployeeName,
                      ClientName            = x.ClientName,
                      BenefitType           = x.BenefitType,
                      BenefitClass          = x.BenefitClass,
                      BenefitCategory       = x.BenefitCategory,
                      AdjustmentDescription = x.Adjustment,
                      Amount                = decimal.Parse(x.Amount.ToString()),
                      Taxable               = x.Taxable,
                      Billable              = x.Billable,
                      EffectiveDate         = DateTime.Parse(x.EffectiveDate.Value.ToShortDateString()),
                      StartDate             = DateTime.Parse(x.StartDate.Value.ToShortDateString()),
                      Remarks               = x.Remarks

                  }).ToList();
        }
    }
}