using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository.Employee
{
    public class EmployeeEmploymentRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeEmploymentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        //============================EXTERNAL EMPLOYMENT================================================
        public PreviousEmploymentModel GetExternalEmployment(int _id)
        {
            return (from d in _conn.REC_CANDIDATE_EMPLOYMENT

                    where d.Employ_ID == _id
                    select d).AsEnumerable()
                  .Select(x => new PreviousEmploymentModel()
                  {
                      Id = x.Employ_ID,
                      CandidateId = x.candidate_id,
                      CompanyId = x.company_id,
                      CompanyName = x.Company_name,
                      CompanyAddress = x.Company_Address,
                      Salary = x.Company_Salary,

                      IndustryId = x.industry_id,
                      IndustryName = x.INDUSTRY.industry_name,

                      FunctionId = x.function_id,
                      FunctionName = "",
                      RoleId = x.role_id,
                      RoleName = "",
                      ReasonForLeaving = x.Reason_for_leaving,

                      Position = x.Company_position,
                      StartDate = x.Company_from,
                      EndDate = x.Company_to,                      

                  }).SingleOrDefault();
        }

        public List<PreviousEmploymentViewModel> GetExternalEmployments(int _canid)
        {
            return (from d in _conn.REC_CANDIDATE_EMPLOYMENT
                    where d.candidate_id == _canid
                    select d).AsEnumerable()
                  .Select(x => new PreviousEmploymentViewModel()
                  {
                      CompanyName = x.Company_name,
                      Position = x.Company_position,
                      EmploymentPeriod = x.Company_from.HasValue ? x.Company_from.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) + "-" + x.Company_to.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : "",
                      Branch = "",
                      Department = "",
                      EmploymentType = ""
                  }).ToList();
        }

        public bool ManageExternal(PreviousEmploymentModel _model)
        {
            try
            {
                _conn.SP_I_MANAGE_EMPLOYMENT_HISTORY(_model.Mode,
                    _model.CandidateId,
                    _model.CompanyName,
                    _model.CompanyAddress,
                    _model.Position,
                    _model.EmploymentRank,
                    _model.ReasonForLeaving,
                    _model.Salary,
                    _model.StartDate,
                    _model.EndDate,
                    _model.Id,
                    _model.UserId,
                    "",
                    "",
                    0,
                    _model.FunctionId,
                    _model.IndustryId,
                    _model.RoleId,
                    _model.JobDescription);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //============================EXTERNAL EMPLOYMENT================================================

        //============================INTERNAL EMPLOYMENT================================================
        public List<InternalEmploymentViewModel> GetInternalEmployments(int _empid)
        {
            return (from d in _conn.REC_NEW_HIRED_EMPLOYEE
                    where d.emp_id == _empid
                    select d).AsEnumerable()
                  .Select(x => new InternalEmploymentViewModel()
                  {
                      Id = x.id,
                      EmpId = x.emp_id,
                      ClientName = x.REC_CLIENT.client_name,
                      Position = x.position,
                      HireType = x.hire_type,
                      EmployeeType = x.EmployeeType.EmpType_Desc,
                      ContractStart = x.contract_start.ToString(),
                      ContractEnd = x.contract_end.ToString(),
                      SeparationDate = x.date_separated.ToString(),
                      InactiveDate = x.date_inactive.ToString(),
                      ContractStatus = x.is_history == true ? "Inactive" : "Active"
                      
                  }).ToList();
        }
        //============================INTERNAL EMPLOYMENT================================================
    }
}