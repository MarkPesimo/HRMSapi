using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRModel.ViewModel.Employees;

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
                        EmpNo = e.Emp_No
                    }).SingleOrDefault();

            return _obj;

        }

        public int ComputeAge(DateTime _bdate)
        {
            if (_bdate == null)
            {
                return 0;
            }
            else
            {
                DateTime _bbdate = DateTime.Parse(_bdate.ToString());
                return (DateTime.Now - _bbdate).Days / 365;
            }
        }
    }
}