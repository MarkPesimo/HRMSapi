using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Floating.Replacement_model;
using static HRModel.ViewModel.Floating.Separation_model;

namespace HRMS_API.Repository
{
    public class SeparationRepository 
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }
        private ReplacementRepository _replacementrepository { get; set; }
        private EmployeeRepository _employeerepository { get; set; }
        private ContractRepository _contractrepository { get; set; }

        public SeparationRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
            if (_replacementrepository == null) { _replacementrepository = new ReplacementRepository(); }
            if (_employeerepository == null) { _employeerepository = new EmployeeRepository(); }
            if (_contractrepository == null) { _contractrepository = new ContractRepository(); }
        }

        public List<SeparationReason_model> GetSeparationReasons()
        {
            return (from d in _conn.Separation_Reason
                    orderby d.Reason
                    select d).AsEnumerable()
               .Select(x => new SeparationReason_model()
               {
                   Id = int.Parse(x.ID.ToString()),
                   Reason = x.Reason
               }).ToList();
        }

        public EmployeeSeparationRecord_model GetEmployeeSeparationRecord(int _id)
        {
            EmployeeSeparationRecord_model _obj = (from d in _conn.Employee_Separation
                                                 where d.ID == _id
                                                 select d).AsEnumerable()
               .Select(x => new EmployeeSeparationRecord_model()
               {
                   Id = int.Parse(x.ID.ToString()),
                   EmpId = x.EmpID,
                   EmployeeName = _employeerepository.GetEmployeeName(x.EmpID),
                   SeparationReason = x.Separation_Reason.Reason,
                   SeparationReasonId = x.Reason_ID,
                   SeparationDate = x.Separation_Date,
                   Remarks = x.Remarks,
                   
                   CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   DateCreated = x.date_modified.ToShortDateString()
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

        public int ManageEmployeeSeparationRecord(EmployeeSeparationRecord_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

                _conn.SP_MANAGE_EMPLOYEE_SEPARATION(_model.Mode, _model.Id, _model.EmpId, _model.SeparationReasonId, _model.UserId,
                                              _model.Remarks, _model.SeparationDate, 
                                              null, false,
                                              null, false, 
                                              null, false, 
                                              null, false,
                                              false, 
                                              "", "", 
                                              0, 0, 
                                              false, null, 
                                              _return_value );

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
