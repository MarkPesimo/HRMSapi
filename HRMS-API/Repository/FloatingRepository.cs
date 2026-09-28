using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Floating.Floating_model;
using static HRModel.ViewModel.Floating.Replacement_model;

namespace HRMS_API.Repository
{
    public class FloatingRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }
        private ReplacementRepository _replacementrepository { get; set; }
        private EmployeeRepository _employeerepository { get; set; }
        private ContractRepository _contractrepository { get; set; }

        public FloatingRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
            if (_replacementrepository == null) { _replacementrepository = new ReplacementRepository(); }
            if (_employeerepository == null) { _employeerepository = new EmployeeRepository(); }
            if (_contractrepository == null) { _contractrepository = new ContractRepository(); }
        }

        public List<FloatingReason_model> GetFloatingReasons()
        {
            return (from d in _conn.FloatingReasons
                    orderby d.floating_reason
                    select d).AsEnumerable()
               .Select(x => new FloatingReason_model()
               {
                   Id = int.Parse(x.id.ToString()),
                   Reason = x.floating_reason
               }).ToList();
        }

        public EmployeeFloatingRecord_model GetEmployeeFloatingRecord(int _id)
        {
            EmployeeFloatingRecord_model _obj = (from d in _conn.Employee_Floating_Status
                                                 where d.id == _id
                                                 select d).AsEnumerable()
               .Select(x => new EmployeeFloatingRecord_model()
               {
                   Id = int.Parse(x.id.ToString()),
                   EmpId = x.emp_id,
                   EmployeeName = _employeerepository.GetEmployeeName(x.emp_id),
                   IsFloating = x.isfloating,
                   FloatingDate = x.floating_date,
                   WithSeparationPay = x.with_separation_pay,
                   Remarks = x.remarks,
                   UserId = x.user_id,
                   CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   DateCreated = x.date_updated.ToShortDateString()
               }).SingleOrDefault();


            if (_obj != null)
            {
                EmployeeReplacement_vw_model _replacement = _replacementrepository.GetReplacementRecord(_obj.Id);
                if (_replacement != null)
                {
                    _obj.ReplacementId = _replacement.Id;
                    _obj.ReplacementDate = _replacement.ReplacementDate;
                    _obj.ReplacementRemarks = _replacement.Remarks;
                }

                //jump to REC_NEW_HIRED_EMPLOYEE where id = _obj.transaction
                //set _obj.ContractStatus = is_history field
                _obj.ContractStatus = _contractrepository.GetContractStatus(_obj.TransactionId);                
            }

            return _obj;
        }

        public int ManageEmployeeFloatingRecord(EmployeeFloatingRecord_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_FLOATING_STATUS(
                    _model.Mode,
                    _model.Id,
                    _model.EmpId,
                    _model.FloatingReasonId,
                    _model.IsFloating,
                    _model.FloatingDate,
                    _model.Remarks,
                    _model.UserId,
                    _return_value,
                    _model.WithSeparationPay,
                    _model.ReplacementDate,
                    _model.ReplacementRemarks);

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