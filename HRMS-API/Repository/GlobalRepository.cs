using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRModel.ViewModel.Employees;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;

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