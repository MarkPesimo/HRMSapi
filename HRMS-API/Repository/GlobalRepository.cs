using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRModel.ViewModel.Employees;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;
using HRModel.ViewModel.Contract.EmployeeTransaction;
using static HRModel.ViewModel.Global.GlobalSearch_models;

namespace HRMS_API.Repository
{
    public class GlobalRepository
    {
        private apwdbEntities _conn { get; set; }

        public GlobalRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

        }

        public EmployeeKeys GetEmployeeKey(string _guid)
        {
            EmployeeKeys _obj = new EmployeeKeys();

            _obj = (from d in _conn.REC_CANDIDATE
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.id equals l.candidate_id
                    join e in _conn.Employees on l.emp_id equals e.Emp_ID
                    where d.guid == _guid
                    select new EmployeeKeys
                    {
                        EmpId = e.Emp_ID,
                        EmpNo = e.Emp_No,
                        EmployeeGUID = _guid
                    }).SingleOrDefault();

            return _obj;

        }

        public List<EmployeeTransaction.ClientList> GetClientList(int _company_id, string _keyword)
        {
            string sanitizedKeyword = string.IsNullOrWhiteSpace(_keyword) || _keyword == "NULL" ? null : _keyword;

            var rawResults = _conn.USP_H_GET_CLIENTS_BY_COMPANY(_company_id).ToList();

            if (!string.IsNullOrEmpty(sanitizedKeyword))
            {
                rawResults = rawResults
                    .Where(x => x.client_name != null && x.client_name.ToLower().Contains(sanitizedKeyword.ToLower()))
                    .ToList();
            }

            List<EmployeeTransaction.ClientList> _obj = rawResults
                .OrderBy(x => x.client_name)
                .Select(x => new EmployeeTransaction.ClientList()
                {
                    Id = x.id ?? 0,
                    ClientName = x.client_name ?? "",
                    Status = x.status ?? false
                }).ToList();

            return _obj;
        }

        public List<EmployeeSearchList> SearchEmployess(int _clientid)
        {
            var query = from d in _conn.Employees
                        join dept in _conn.Departments on d.Department_ID equals dept.Dept_ID
                        select new { d, dept };

            if (_clientid != 0)
            {
                query = query.Where(x => x.d.client_id == _clientid);
            }
            
            return query.AsEnumerable().Select(x => new EmployeeSearchList
            {
                EmpID = x.d.Emp_ID,
                EmpNo = x.d.Emp_No,
                LastName = x.d.Lastname,
                FirstName = x.d.Firstname,
                MiddleName = x.d.Middlename,
                ClientName = x.d.REC_CLIENT != null ? x.d.REC_CLIENT.client_name : "",
                Branch = Convert.ToInt16(x.d.Branch.no_of_days).ToString() + " - " + x.d.Branch.Branch_Desc,
                EmpStatus = Convert.ToInt32(x.d.ActiveInactive),
                IsIncludePayroll = Convert.ToBoolean(x.d.IsIncludePayroll),
                RankID = x.d.EmpRank_ID,
                Position = x.d.Position,
                ContractType = x.d.REC_CONTRACT_TYPE != null ? x.d.REC_CONTRACT_TYPE.contract_type : "",
                Department = x.dept.Dept_Name,
                Datehired = Convert.ToDateTime(x.d.Datehired),
                Paytype = x.d.pay_type,
                SourceType = x.d.REC_SOURCE_TYPE != null ? x.d.REC_SOURCE_TYPE.source_type : "",
                BranchID = x.d.Branch_ID,
                EmployerId = x.d.employer_id,
                EmployerName = x.d.REC_CLIENT != null ? x.d.REC_CLIENT.client_name : ""
            }).ToList();
        }

        public List<EmployeeTypeModel> GetEmployeeTypes()
        {
            var query = from et in _conn.EmployeeTypes
                        select et;

            return query.AsEnumerable().Select(x => new EmployeeTypeModel
            {
                EmpTypeID = x.EmpType_ID,
                EmpTypeDesc = x.EmpType_Desc,
                UserID = x.UserID,
                Status = Convert.ToBoolean(x.status),
                DateCreated = Convert.ToDateTime(x.date_created)
            }).ToList();
        }

        public List<EmployeeRankModel> GetEmployeeRanks()
        {
            var query = from er in _conn.EmployeeRanks
                        where er.Status == true
                        select er;

            return query.AsEnumerable().Select(x => new EmployeeRankModel
            {
                EmpRankID = x.EmpRank_ID,
                EmployeeRank = x.EmployeeRank1,
                UserID = x.Userid,
                Status = Convert.ToBoolean(x.Status),
                DateCreated = Convert.ToDateTime(x.Date_created)
            }).ToList();
        }

        public List<DepartmentModel> GetClientDepartments(int clientId)
        {
            var query = from cd in _conn.REC_CLIENT_DEPARTMENT
                        where cd.client_id == clientId && cd.status == true
                        orderby cd.Department.Dept_Name 
                        select new DepartmentModel
                        {
                            DeptID = cd.department_id,
                            DeptName = cd.Department.Dept_Name
                        };

            return query.ToList();
        }

        public List<BranchModel> GetClientBranches(int clientId)
        {
            var query = from b in _conn.Branches
                        where b.client_id == clientId && b.status == true
                        orderby b.Branch_Desc
                        select new BranchModel
                        {
                            BranchID = b.Branch_ID,
                            BranchDesc = b.Branch_Desc,
                            ClientID = b.client_id,
                            Status = b.status 
                        };

            return query.ToList();
        }

        public List<ShiftModel> GetClientShifts(int clientId)
        {
            var query = from d in _conn.Shifts
                        join f in _conn.REC_CLIENT_SHIFT on d.Shift_ID equals f.shift_id
                        where f.client_id == clientId && f.status == true
                        orderby d.Description descending
                        select new ShiftModel
                        {
                            ShiftID = d.Shift_ID,
                            Description = d.Description
                        };

            return query.ToList();
        }

        public int ManageContract(Contract _model, int _mode)
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
                _mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }

        public EmployeeKeys GetEmployeeKeyReverse(int _empid)
        {
            EmployeeKeys _obj = new EmployeeKeys();

            _obj = (from d in _conn.REC_CANDIDATE
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.id equals l.candidate_id
                    join e in _conn.Employees on l.emp_id equals e.Emp_ID
                    where e.Emp_ID == _empid
                    select new EmployeeKeys
                    {
                        EmpId = e.Emp_ID,
                        EmpNo = e.Emp_No,
                        EmployeeGUID = d.guid
                    }).SingleOrDefault();

            return _obj;

        }

        public string GetMonthName(int _month)
        {
            string _monthname = "Jan";

            if (_month == 1) { _monthname = "Jan"; }
            else if (_month == 2) { _monthname = "Feb"; }
            else if (_month == 3) { _monthname = "Mar"; }
            else if (_month == 4) { _monthname = "Apr"; }
            else if (_month == 5) { _monthname = "May"; }
            else if (_month == 6) { _monthname = "Jun"; }
            else if (_month == 7) { _monthname = "Jul"; }
            else if (_month == 8) { _monthname = "Aug"; }
            else if (_month == 9) { _monthname = "Sep"; }
            else if (_month == 10) { _monthname = "Oct"; }
            else if (_month == 11) { _monthname = "Nov"; }
            else if (_month == 12) { _monthname = "Dec"; }

            return _monthname;
        }

        public void ManageEmployeeTransactionLog(TransactionLog _log)
        {
            _conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION_BACKDATE_LOG(_log.TranId,
                _log.TranAction,
                _log.UserId,
                _log.DateValue);

        }
   
    }
}