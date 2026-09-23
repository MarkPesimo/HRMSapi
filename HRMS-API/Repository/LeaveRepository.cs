using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRModel.ViewModel.Leave;
using static HRModel.ViewModel.Leave.Leave_model;

namespace HRMS_API.Repository
{
    public class LeaveRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        //private LeaveRepository _leaverepository { get; set; }

        public LeaveRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }
        
        public List<EmployeeLeaveBalanceMonitoring_model> GetEmployeeLeaveBalanceMonitoring(LeaveBalanceFilter_model _filter)
        {
            return (from x in _conn.USP_H_GET_LEAVE_BALANCE_MONITORING(
                        _filter.YearEntitled,
                        _filter.ByClient,
                        _filter.ClientId,
                        _filter.ByLeaveType,
                        _filter.LeaveTypeId,
                        _filter.Keyword ?? "",
                        _filter.CompanyId,
                        _filter.EmpId)
                    select x).AsEnumerable()
                  .Select(x => new EmployeeLeaveBalanceMonitoring_model()
                  {
                      Id = int.Parse(x.id.ToString()),
                      ClientName = x.client_name,
                      EmployeeName = x.employee_name,
                      LeaveType = x.leave_type,
                      EntitledLeave = x.entitled_leave.ToString(),
                      EarnedPerMonth = x.earned_per_month.ToString(),
                      AvailableLeave = x.available_leave.ToString(),
                      UsedLeave = x.used_leave.ToString(),
                      Balance = x.balance_leave.ToString(),
                      AddedBy = x.added_by,
                      DateAdded = x.date_added.Value.ToShortDateString(),
                      Status = x.status,
                      YearEntitled = _globalrepository.GetMonthName(int.Parse(x.month_entitled.ToString())) + "-" + x.year_entitled.ToString(),
                      ClientId = int.Parse(x.client_id.ToString()),
                      EmpId = int.Parse(x.emp_id.ToString()),
                      WhenCreditisEarned = x.when_credit_is_earned,
                      EmployeeGUID  = _globalrepository.GetEmployeeKeyReverse(int.Parse( x.emp_id.ToString())).EmployeeGUID
                  }).ToList();
        }

        public LeaveBalanceModel GetEmployeeLeaveBalance(int _id)
        {
            return (from d in _conn.EmployeeLeaveMonitorings
                    where d.ID == _id
                    select d).AsEnumerable()
                .Select(x => new LeaveBalanceModel()
                {
                    Id = x.ID,
                    EmpId = x.EmpID,
                    EmployeeName = x.Employee.Lastname + ", " + x.Employee.Firstname,
                    LeaveTypeId = x.LeaveTypeID,
                    EntitleLeave = x.EntitleLeave,
                    RemainingLeave = x.RemainingLeave,
                    EarnedLeave = x.RemainingLeave,
                    BalanceLeave = x.BalanceLeave,
                    UsedLeave = x.UsedLeave,
                    YearEntitled = x.YearEntitled,
                    MonthEntitled = x.MonthEntitled,
                    ValidFrom = x.ValidDateFrom,
                    ValidTo = x.ValidDateTo,
                    UserId = x.UserId,
                    DateAdded = x.date_added,
                    ClientId = x.client_id,
                    ClientName = x.REC_CLIENT.client_name,
                    CreditEarnedPerMonth = x.credit_earned_per_month,
                    IsConvertable = x.is_convertable,
                    AutoResetPerYear = x.auto_reset_per_year,
                    WhenCreditIsEarned = x.when_credit_is_earned
                }).SingleOrDefault();
        }

        public int ManageEmployeeLeaveBalance(LeaveBalanceModel _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MANAGE_EMPLOYEE_LEAVE_MONITORING(_model.Id,
                    _model.EmpId,
                    _model.LeaveTypeId,
                    _model.EntitleLeave,
                    _model.UsedLeave,
                    _model.RemainingLeave,

                    _model.YearEntitled,
                    _model.MonthEntitled,
                    _model.ClientId,
                    _model.Mode,
                    _model.UserId,
                    _model.ValidDateFrom,
                    _model.ValidDateTo,
                    _model.CreditEarnedPerMonth,
                    _model.IsConvertable,
                    _model.AutoResetPerYear,
                    _model.WhenCreditIsEarned,
                    _model.EarnedLeave,
                    _model.BalanceLeave,
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

        public int ManageEmployeeFiledLeave(LeaveModel _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MASTER_P_LEAVE(
                    _model.Id,
                    _model.EmpId,
                    _model.LeaveTypeId,
                    _model.EmergencyLeave,
                    _model.DateFiled,
                    _model.LeaveFrom,
                    _model.LeaveTo,
                    _model.LeaveFromAMPM,
                    _model.LeaveToAMPM,
                    _model.LeaveDays,
                    _model.Reason,
                    _model.Remarks,
                    _model.IsHalfday,
                    true,
                    _model.FirstHalf,
                    _model.SecondHalf,
                    _model.FirstDay_SecondHalf,
                    _model.LastDay_FirstHalf,
                    _model.Mode,
                    _model.UserId,
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

        public List<EmployeeFiledLeave> GetEmployeeFiledLeaveMonitoring(LeaveFilter_model _filter)
        {
            return (from d in _conn.USP_H_GET_EMPLOYEE_FILED_LEAVE(
                _filter.ClientId, 
                _filter.Status,
                _filter.ByMonthYear,
                _filter.MonthLeave,
                _filter.YearLeave,
                _filter.ByDate,
                _filter.DateFrom,
                _filter.DateTo,
                _filter.keyword,
                _filter.CompanyId
                )
                    select d).AsEnumerable()
                .Select(x => new EmployeeFiledLeave()
                {
                    Id = int.Parse(x.id.ToString()),
                    ClientName = x.client_name,
                    EmployeeName = x.employee_name,
                    DateFiled = x.date_filed.Value.ToShortDateString(),
                    LeaveType = x.leave_type,
                    LeaveDays = x.leave_days.ToString(),
                    Status = x.status,
                    CreatedBy = x.created_by,
                    Reason = x.reason,
                    LeaveFrom = x.leave_from.Value.ToShortDateString(),
                    LeaveTo = x.leave_to.Value.ToShortDateString()
                }).ToList();
        }

        public List<LeaveTypeModel> GetActiveLeaveTypes()
        {
            try
            {
                return (from lt in _conn.LeaveTypes
                        where lt.status == true
                        orderby lt.Leavetype_desc ascending
                        select new LeaveTypeModel
                        {
                            LeaveTypeId = lt.Leavetype_ID,
                            LeaveTypeDesc = lt.Leavetype_desc,
                            LeaveCode = lt.LeaveCode
                        }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeLeaveDetailMonitoring_model GetEmployeeLeaveDetail(int _id)
        {
            var _obj = (from elm in _conn.EmployeeLeaveMonitorings
                        join e in _conn.Employees on elm.EmpID equals e.Emp_ID
                        join lt in _conn.LeaveTypes on elm.LeaveTypeID equals lt.Leavetype_ID into ltGroup
                        from lt in ltGroup.DefaultIfEmpty()
                        join c in _conn.REC_CLIENT on elm.client_id equals c.id into cGroup
                        from c in cGroup.DefaultIfEmpty()
                        where elm.ID == _id
                        select new EmployeeLeaveDetailMonitoring_model
                        {
                            Id = elm.ID,
                            EmpId = elm.EmpID,
                            EmpNo = e.Emp_No,
                            EmployeeName = e.Firstname + " " + e.Lastname,
                            ClientName = c != null ? c.client_name : "-",

                            LeaveTypeId = elm.LeaveTypeID,
                            LeaveType = lt != null ? lt.Leavetype_desc : "-",

                            EntitleLeave = elm.EntitleLeave,
                            RemainingLeave = elm.RemainingLeave,
                            UsedLeave = elm.UsedLeave,
                            BalanceLeave = elm.BalanceLeave,

                            MonthEntitled = elm.MonthEntitled,
                            YearEntitled = elm.YearEntitled,
                            ValidDateFrom = elm.ValidDateFrom,
                            ValidDateTo = elm.ValidDateTo,

                            UserId = elm.UserId,
                            ClientId = elm.client_id,
                            Status = elm.status,
                            DateAdded = elm.date_added,
                            CreditEarnedPerMonth = elm.credit_earned_per_month,
                            IsConvertable = elm.is_convertable,
                            AutoResetPerYear = elm.auto_reset_per_year,
                            WhenCreditIsEarned = elm.when_credit_is_earned
                        }).FirstOrDefault();

            return _obj;
        }

        public int ManageLeaveAccept(LeaveAcceptModel _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_LEAVE_ACCEPT(
                    _model.Id,
                    _model.PLeaveId,
                    _model.DateAccepted,
                    _model.Remarks,
                    _model.Mode,
                    _model.UserId,
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

        public LeaveModel GetEmployeeFiledLeave(int _id)
        {
            return (from d in _conn.P_Leave
                    where d.P_LeaveID == _id
                    select d).AsEnumerable()
                .Select(x => new LeaveModel()
                {
                    Id = x.P_LeaveID,
                    EmpId = x.EmpID,
                    EmpName = x.Employee != null ? x.Employee.Lastname + ", " + x.Employee.Firstname : "",
                    ClientName = x.REC_CLIENT != null ? x.REC_CLIENT.client_name : "",
                    LeaveTypeId = x.LeaveTypeId,
                    LeaveType = x.LeaveType != null ? x.LeaveType.Leavetype_desc : "",
                    EmergencyLeave = x.is_EmergencyLeave,
                    DateFiled = x.DateFiled,
                    LeaveFrom = x.LeaveFrom,
                    LeaveFromAMPM = x.FromAMPM,
                    LeaveTo = x.LeaveTo,
                    LeaveToAMPM = x.ToAMPM,
                    LeaveDays = x.LeaveDays,
                    IsHalfday = x.IsHalfDay,
                    Reason = x.Reason,
                    Remarks = x.Remarks,
                    Status = x.Status,
                    FileStatus = x.FileStatus,
                    IsRejected = x.IsRejected,
                    UserId = x.user_modified ?? 0
                }).SingleOrDefault();
        }

        public int ManageLeaveRevoke(LeaveAcceptModel _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_LEAVE_REVOKE(
                    _model.Id,
                    _model.PLeaveId,
                    _model.DateAccepted,
                    _model.Remarks,
                    _model.Mode,
                    _model.UserId,
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