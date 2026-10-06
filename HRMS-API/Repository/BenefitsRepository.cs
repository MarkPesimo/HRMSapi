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
        private GlobalRepository _globalrepository { get; set; }
        private ClientAdjustmentRepository _clientadjustmentrepository { get; set; }
        private UserRepository _userrepository { get; set; }

        public BenefitsRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_clientadjustmentrepository == null) { _clientadjustmentrepository = new ClientAdjustmentRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
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
            _obj.Add(_benefits);

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
            _obj.Add(_benefits);

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

        public BenefitsRecord_model GetBenefitsRecord(int _id)
        {
            BenefitsRecord_model _obj = new BenefitsRecord_model();

            _obj = (from d in _conn.Employee_Benefits
                    join t in _conn.Employee_Benefits_Type on d.benefit_type_id equals t.id
                    join c in _conn.REC_CLIENT on d.client_id equals c.id
                    where d.id == _id
                    select d).AsEnumerable()
                  .Select(x => new BenefitsRecord_model()
                  {
                      Id                    = x.id,
                      EmpId                 = x.emp_id,
                      EmployeeName          = x.Employee.Lastname + ", " + x.Employee.Firstname + " " + x.Employee.Middlename,
                      BenefitTypeId         = x.benefit_type_id,
                      BenefitType           = x.Employee_Benefits_Type.benefits_type,
                      BenefitClassId        = x.benefit_class_id,
                      BenefitClass          = _globalrepository.GetBenefitsClass(x.benefit_class_id),
                      BenefitCatId          = x.benefit_cat_id,
                      BenefitCategory       = _globalrepository.GetBenefitsCategory(x.benefit_cat_id),
                      AdjustmentId          = int.Parse(x.adj_id.ToString()),
                      AdjustmentDescription = _clientadjustmentrepository.GetAdjustment(int.Parse(x.adj_id.ToString())).Description,
                      ClientId              = x.client_id,
                      ClientName            = x.REC_CLIENT.client_name,
                      Amount                = decimal.Parse(x.amount.ToString()),
                      EffectiveDate         = x.effective_date,
                      StartDate             = x.benefits_start_date,
                      IsTaxable             = bool.Parse(x.is_taxable.ToString()),
                      IsBillable            = bool.Parse(x.is_billable.ToString()),
                      Remarks               = x.remarks,
                      Status                = bool.Parse(x.benefit_status.ToString()),
                      UserCreatedId         = int.Parse(x.user_created.ToString()),
                      CreatedBy             = _userrepository.GetUser(int.Parse(x.user_created.ToString())).UserName,
                      DateCreated           = x.date_created.HasValue ? x.date_created.Value.ToString() : "",
                      UserModifiedId        = x.user_modified.HasValue ? int.Parse(x.user_modified.ToString()) : 0,
                      ModifiedBy            = x.user_modified.HasValue ? _userrepository.GetUser(int.Parse(x.user_modified.ToString())).UserName : "",
                      DateModified          = x.date_modified.HasValue ? x.date_modified.Value.ToString() : ""
                  }).SingleOrDefault();

            return _obj;
        }

        public int ManageBenefitsRecord(BenefitsRecord_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_BENEFITS(
                    _model.Mode,
                    _model.Id,
                    _model.EmpId,
                    _model.BenefitTypeId,
                    _model.BenefitClassId,
                    _model.BenefitCatId,
                    _model.Amount,
                    _model.EffectiveDate,
                    _model.StartDate,
                    _model.AdjustmentId,
                    _model.IsTaxable,
                    _model.IsBillable,
                    _model.Status,
                    _model.Remarks,
                    _model.UserCreatedId,
                    _return_value);

                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }
    }
}