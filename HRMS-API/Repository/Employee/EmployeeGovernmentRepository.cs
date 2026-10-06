using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository.Employee
{
    public class EmployeeGovernmentRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeGovernmentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        //public GovernmentNos GetGovernmentInfo(int _id)
        //{
        //    return (from x in _conn.Employees
        //            where x.Emp_ID == _id
        //            select x
        //        ).AsEnumerable()
        //        .Select(d => new GovernmentNos()
        //        {
        //            EmpId = int.Parse(d.Emp_ID.ToString()),
        //            SSSNo = d.SSS_no,
        //            PhilhealthNo = d.Philhealth_no,
        //            PagibigNo = d.Pagibig_no,
        //            TINNo = d.Tax_no,
        //            UserId = d.UserID,
        //            CreatedBy = d.SYS_USER.username,
        //            DateCreated = d.date_encoded
        //        }).SingleOrDefault();
        //}

        //public int Manage(GovernmentNos _model)
        //{
        //    System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

        //    _conn.USP_H_MANAGE_EMPLOYEE_GOVERNMENT(
        //        _model.EmpId,
        //        _model.PhilhealthNo,
        //        _model.PagibigNo,
        //        _model.TINNo,
        //        _model.SSSNo,
        //        _model.Mode,
        //        _model.UserId,
        //        _return_value
        //    );

        //    return Convert.ToInt32(_return_value.Value);
        //}
    }
}