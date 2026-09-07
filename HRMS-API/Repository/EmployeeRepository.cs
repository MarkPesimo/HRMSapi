using HRModel.ViewModel.Employees;
using HRMS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository
{
    public class EmployeeRepository
    {
        private APWDBEntities _conn { get; set; }

        public EmployeeRepository()
        {
            if (_conn == null) { _conn = new APWDBEntities(); }


        }

        public EmployeeProfile GetEmployeeProfile(string _guid)
        {

        }
    }
}