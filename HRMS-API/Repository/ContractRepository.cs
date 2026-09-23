using HRModel.ViewModel.Contract.EmployeeTransaction;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Contract.EmployeeTransaction.EmployeeTransaction;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;
using static HRModel.ViewModel.Floating.Floating_model;
using static HRModel.ViewModel.Floating.Separation_model;
using static HRModel.ViewModel.Global.GlobalSearch_models;

namespace HRMS_API.Repository
{
    public class ContractRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }


        public ContractRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }

        }

        public List<EmployeeTransaction.Monitoring> GetMonitoring(int _clientid, string _keyword, bool _bydate, string _stage, DateTime _from, DateTime _to, bool _bycontractstatus, bool _contractstatus, bool _byemployeetype, int _employeetypeid, bool _byuserhired, int _userid, int _company_id)
        {
            string sanitizedKeyword = string.IsNullOrWhiteSpace(_keyword) || _keyword == "NULL" ? null : _keyword;
            string sanitizedStage = string.IsNullOrWhiteSpace(_stage) ? "CONTRACT DATE" : _stage;

            var rawResults = _conn.USP_H_EMPLOYEE_TRANSACTION_MONITORING(
                _clientid,
                sanitizedKeyword,
                _bydate,
                sanitizedStage,
                _from,
                _to,
                _bycontractstatus,
                _contractstatus,
                _byemployeetype,
                _employeetypeid,
                _byuserhired,
                _userid,
                _company_id
            ).ToList();

            List<EmployeeTransaction.Monitoring> _obj = rawResults
                .OrderBy(x => x.employee_name)
                .Select(x => new EmployeeTransaction.Monitoring()
                {
                    Id = x.id ?? 0,
                    EmpId = x.emp_id ?? 0,
                    EmployeeName = x.employee_name ?? "",
                    ClientName = x.client_name ?? "",
                    HireType = x.hire_type ?? "",
                    DateHired = x.date_hired?.ToShortDateString() ?? "",
                    ContractStart = x.contract_start?.ToShortDateString() ?? "",
                    ContractEnd = x.contract_end?.ToShortDateString() ?? "",
                    HiredBy = x.hired_by ?? "",
                    JoDetId = x.jo_det_id ?? 0,
                    DateResignationSubmitted = x.date_resignation_submitted?.ToShortDateString() ?? "",
                    DateSeparated = x.date_separated?.ToShortDateString() ?? "",
                    DateInactive = x.date_inactive?.ToShortDateString() ?? "",
                    DateInactiveCreated = x.date_inactive_created?.ToShortDateString() ?? "",
                    FloatingId = x.floating_id ?? 0,
                    SeparationId = x.separated_id ?? 0,
                    ContractStatus = x.contract_status ?? "",
                    EmployeeType = x.employee_type ?? "",
                    Position = x.position ?? "",
                    DateRegularized = x.date_regular?.ToShortDateString() ?? "",
                    ClientId = x.client_id ?? 0,
                    ContractExtended = x.contract_extended == true ? "Yes" : "No"
                }).ToList();

            return _obj;
        }

        public int ManageContract(Contract _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("ReturnId", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION(
                _model.Id,
                _model.ClientId,
                _model.EmpId,
                _model.DateHired,
                _model.ContractStart,
                _model.ContractEnd,
                _model.HiredById,
                _model.HireType,
                _model.JoDetId,
                _model.CurrentContract,
                _model.EmployeeTypeId,
                _model.Position,
                _model.DateRegularized,
                _model.DepartmentId,
                _model.BranchId,
                DateTime.Now,
                DateTime.Now,
                0,
                0,
                DateTime.Now,
                DateTime.Now,
                0,
                0,
                _model.EmployeeRankId,
                _model.ShiftId,
                _model.mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }

        public int ManageEmployeeContractFloating(Floating _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION(_model.Id,
                    0,
                    0,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    _model.LoginUserId,
                    "",
                    0,
                    false,
                    0,
                    "",
                    DateTime.Now,
                    0,
                    0,
                    _model.DateResignationSubmitted,
                    _model.DateSeparated,
                    _model.SeparatedById,
                    _model.FloatingId,
                    DateTime.Now,
                    DateTime.Now,
                    0, 0, 0, 0,
                    _model.Mode,
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

        public int ManageEmployeeContractSeparation(Separation _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION(_model.Id,
                    0,
                    0,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    _model.LoginUserId,
                    "",
                    0,
                    false,
                    0,
                    "",
                    DateTime.Now,
                    0,
                    0,
                    DateTime.Now,
                    DateTime.Now,
                    0,
                    0,
                    _model.DateInactive,
                    _model.DateCreated,
                    _model.InactiveById,
                    _model.SeparationId,
                    0, 0,
                    _model.Mode,
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

        public bool GetContractStatus(int _id)
        {
            REC_NEW_HIRED_EMPLOYEE _obj = (from d in _conn.REC_NEW_HIRED_EMPLOYEE where d.id == _id select d).SingleOrDefault();
            if (_obj != null)
            {
                return _obj.is_history;
            }            
            else { return false; }
        }

        public int ManageRehire(EmployeeRehire_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

                _conn.SP_MANAGE_EMPLOYEE(_model.EmpId,
                    "", "", "", "", null, null,
                    null, null, null, null, null, null,
                    null, null, null, null, null,
                    null, null, null, null, null, null,
                    _model.CompanyId, _model.DepartmentId, _model.Position, _model.EmployeeRankId, _model.EmployeeTypeId, _model.ShiftId, _model.BranchId, _model.SalaryTypeId,
                    null, null, null, null, null,
                    null, 0, null, 
                    4,              //mode
                    _model.UserId,
                    _return_value,
                    null, null, null, null,
                    null, 0, 0,
                    _model.ClientId, _model.PayType,
                    0, 1, 1,
                    _model.MonthlySalary,
                    0, 0, 0, 0, 0, 0,
                    _model.EmployerId, _model.IsMinumum,
                    false, false, 0, 0);



                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }


        //==========================CONTRACT REMARKS==============================
        public List<ContractRemarks> GetContractRemarks(int _tranid)
        {
            return (from d in _conn.REC_NEW_HIRED_EMPLOYEE_REMARKS
                    where d.tran_id == _tranid
                    select d).AsEnumerable()
                .Select(x => new ContractRemarks()
                {
                    Id = x.id,
                    TranId = x.tran_id,
                    DateCreated = x.date_created,
                    UserId = x.user_id,
                    Username = x.SYS_USER.username,
                    Remarks = x.remarks
                }).ToList();
        }

        public ContractRemarks GetContractRemark(int _id)
        {
            return (from d in _conn.REC_NEW_HIRED_EMPLOYEE_REMARKS
                    where d.id == _id
                    select d).AsEnumerable()
                .Select(x => new ContractRemarks()
                {
                    Id = x.id,
                    TranId = x.tran_id,
                    DateCreated = x.date_created,
                    UserId = x.user_id,
                    Username = x.SYS_USER.username,
                    Remarks = x.remarks
                }).SingleOrDefault();
        }

        public int ManageContractRemarks(ContractRemarks _model)
        {
            string _return = "";

            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION_REMARKS(_model.Id,
                _model.TranId,
                _model.UserId,
                _model.Remarks,
                _model.Mode,
                _return_value);

            _return = Convert.ToString(_return_value.Value);

            return int.Parse(_return.ToString());
        }
        //==========================CONTRACT REMARKS==============================
    }
}