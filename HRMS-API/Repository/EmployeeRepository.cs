using HRModel.ViewModel.Employees;
//using HRMS_API.Models;
using HRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.DB;
using static HRModel.ViewModel.Contract.EmployeeTransaction.EmployeeTransaction;

namespace HRMS_API.Repository
{
    public class EmployeeRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<EmployeeMonitoringViewModel> GetEmployeeMonitoring(string Keyword, bool ByClient, int ClientID, int PageNo, int PageSize, int CompanyID)
        {
            if (Keyword == "NULL") { Keyword = ""; } 
            return (from d in _conn.USP_H_GET_EMPLOYEE_MONITORING(Keyword, ByClient, ClientID, PageNo, PageSize, CompanyID)
                    select d).AsEnumerable()
                  .Select(x => new EmployeeMonitoringViewModel()
                  {
                      GUID = x.GU_ID,
                      EmployeeID = x.EmpID.ToString(),
                      EmployeeName = x.LastName + ", " + x.FirstName + " " + x.MiddleName,
                      EmployerName = x.EmployerName,
                      Client = x.ClientName,
                      Branch = x.Branch,
                      Department = x.Department,
                      Position = x.Position,
                      EmployeeType = "",
                      SourceType = x.SourceType,
                      PayType = x.Paytype,
                      DateHired = DateTime.Parse(x.Datehired.Value.ToShortDateString()),
                      UserEncoded = x.UserEncoded,
                      DateEncoded = DateTime.Parse(x.DateEncoded.Value.ToShortDateString()) 

                  }).ToList();
        }

        public EmployeeProfile GetEmployeeProfile(string GuId)
        {
            //get empid
            int _empid = _globalrepository.GetEmployeeKey(GuId).EmpId;
            EmployeeProfile _profile = new EmployeeProfile
            {
                Personal = GetPersonalInfo(_empid),
                //Spouse = GetSpouseInfo(_empid)
            };

            return _profile;
        }

        public PersonalInfo GetPersonalInfo(int _empid)
        {
            PersonalInfo _obj = new PersonalInfo();

            _obj = (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select new PersonalInfo
                    {
                        EmployeeNo      = d.Emp_No,
                        LastName        = d.Lastname,
                        FirstName       = d.Firstname,
                        MiddleName      = d.Middlename,
                        BirthDate       = DateTime.Parse(d.Birthday.Value.ToShortDateString()),
                        Gender          = d.Gender,
                        CivilStatus     = d.CivilStatus,
                        Nationality     = d.Citizenship,
                        BirthPlace      = d.Birthplace,
                        EmailAdd        = d.EmailAddress,
                        PresentAdd      = d.Address,
                        ProvincialAdd   = d.Prov_Address
                    }).SingleOrDefault();

            return _obj;
        }

        public SpouseInfo GetSpouseInfo(int _empid)
        {
            SpouseInfo _obj = new SpouseInfo();

            _obj = (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select new SpouseInfo
                    {
                        SpouseName = d.Spouse,
                        SpouseCompany = d.Spouse_comp,
                        SpouseCompanyAdd = d.Spouse_address
                    }).SingleOrDefault();

            return _obj;
        }
               
        public string GetEmployeeName(int _empid)
        {
            return (from d in _conn.Employees where d.Emp_ID == _empid select d.Lastname + ", " + d.Firstname).ToString();
        }

       
    }
}