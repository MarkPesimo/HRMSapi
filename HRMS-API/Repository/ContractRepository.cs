using HRModel.ViewModel.Contract.EmployeeTransaction;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.GlobalSearch_models;

namespace HRMS_API.Repository
{
    public class ContractRepository
    {
        private apwdbEntities _conn { get; set; }

        public ContractRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
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

        public Contract GetContract(int _id)
        {
            return (from d in _conn.REC_NEW_HIRED_EMPLOYEE
                    where d.id == _id
                    select d).AsEnumerable()
                .Select(x => new Contract()
                {
                    Id = x.id,
                    ClientId = x.client_id,
                    ClientName = x.REC_CLIENT.client_name,
                    EmpId = x.emp_id,
                    EmployeeName = x.Employee.Lastname + ", " + x.Employee.Firstname,
                    DateHired = x.date_hired,
                    ContractStart = x.contract_start,
                    ContractEnd = x.contract_end,
                    HireType = x.hire_type,
                    EmployeeTypeId = x.employee_type_id,
                    EmployeeRankId = x.employee_rank_id,
                    CurrentContract = !x.is_history,
                    HiredById = x.user_id,
                    HiredBy = x.SYS_USER.username,
                    JoDetId = x.jo_det_id,
                    Position = x.position,
                    DateCreated = x.date_created.ToShortDateString(),
                    DateRegularized = x.date_regularized,
                    DepartmentId = x.department_id,
                    BranchId = x.branch_id,
                    RestDayId = x.restday_id,
                    ShiftId = x.shift_id,
                    IsContractExtended = x.contract_extended == false ? "No" : "Yes",
                    DateExtended = x.date_contract_extended == null ? "" : x.date_contract_extended.Value.ToShortDateString()
                }).SingleOrDefault();
        }
    }
}