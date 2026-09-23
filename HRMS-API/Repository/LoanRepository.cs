using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Loan.Loan_model;

namespace HRMS_API.Repository
{
    public class LoanRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        
        public LoanRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }        
        }

        public List<EmployeeLoanMonitoringModel> GetEmployeeLoanMonitoring(LoanFilter_model _filter)
        {
            return (from d in _conn.USP_H_GET_LOAN_MONITORING(
                   _filter.ByLoan,
                   _filter.LoanTypeId,
                   _filter.ByDate,
                   _filter.From,
                   _filter.To,
                   _filter.ByStatus,
                   _filter.Status,
                   _filter.Keyword,
                   _filter.CompanyId
               )
                    select d).AsEnumerable()
               .Select(x => new EmployeeLoanMonitoringModel()
               {
                   Id = int.Parse(x.id.ToString()),
                   LoanGUID = x.loanGUID,
                   LoanType = x.loan_typ,
                   LoanDate = x.date_loan.Value.ToShortDateString(),
                   ClientName = x.client_name,
                   EmployeeName = x.employee_name,
                   Remarks = x.remarks,
                   LoanAmount = x.loan_amount.ToString(),
                   Balance = x.balance.ToString(),
                   Deducted = x.deducted.ToString(),                   
                   Status = x.status,
                   CreatedBy = x.created_by,
               }).ToList();
        }

        public int ManageEmployeeLoan(EmployeeLoanModel _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_EMPLOYEE_LOAN_MASTER(
                    _model.Id,
                    _model.EmpId,
                    _model.LoanTypeId,
                    _model.LoanDate,
                    _model.LoanAmount,
                    _model.Balance,
                    _model.DeductionAmount,
                    _model.LoanStatus,
                    _model.ForDeduction,
                    _model.DeductionAmount,
                    _model.Remarks,
                    _model.Mode,
                    _model.UserId,
                    _return_value, 
                    _model.LoanStartDate);

                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }

        public List<LoanTypeModel> GetLoanTypes()
        {
            return _conn.LoanTypes
                .AsEnumerable()
                .Select(x => new LoanTypeModel
                {
                    LoanTypeID = x.LoanType_ID,
                    LoanTypeDesc = x.LoanType_Desc,
                    UserID = x.UserID,
                    DateCreated = Convert.ToDateTime(x.date_created)
                })
                .ToList();
        }
    }
}