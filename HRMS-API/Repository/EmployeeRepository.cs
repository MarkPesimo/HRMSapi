using HRModel.ViewModel.Employees;
//using HRMS_API.Models;
using HRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.DB;

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
                Spouse = GetSpouseInfo(_empid),
                EmergencyContact = GetEmergencyContact(_empid),
                CurrentEmployment = GetCurrentEmployment(_empid)
            };

            return _profile;
        }

        public PersonalInfo GetPersonalInfo(int _empid)
        {
            PersonalInfo _obj = new PersonalInfo();

            _obj = (from d in _conn.Employees
                    join a in _conn.AreaLibraries on d.CityID equals a.AreaID
                    join l in _conn.AreaLibraries on d.ProvinceID equals l.AreaID
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                  .Select(x => new PersonalInfo()
                    {
                        EmployeeNo      = x.Emp_No,
                        LastName        = x.Lastname,
                        FirstName       = x.Firstname,
                        MiddleName      = x.Middlename,
                        BirthDate       = DateTime.Parse(x.Birthday.Value.ToShortDateString()),
                        Age             = _globalrepository.ComputeAge(x.Birthday.Value),
                        Gender          = x.Gender,
                        CivilStatus     = x.CivilStatus,
                        Nationality     = x.Citizenship,
                        BirthPlace      = x.Birthplace,
                        EmailAdd        = x.EmailAddress,
                        PresentAdd      = x.Address,
                        City            = x.AreaLibrary.AreaDescription,
                        ProvincialAdd   = x.Prov_Address,
                        Province        = x.AreaLibrary1.AreaDescription,
                        Skills          = "",
                        CityId          = x.CityID,
                        ProvinceId      = x.ProvinceID
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

        public EmergencyContactInfo GetEmergencyContact(int _empid)
        {
            EmergencyContactInfo _obj = new EmergencyContactInfo();

            _obj = (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select new EmergencyContactInfo
                    {
                        ContactPerson   = d.Contact_person,
                        ContactAdd      = d.Contact_Address,
                        ContactNo       = d.Contact_No,
                        ContactRelation = d.Contact_relation
                    }).SingleOrDefault();

            return _obj;
        }

        public EmploymentInfo GetCurrentEmployment(int _empid)
        {
            EmploymentInfo _obj = new EmploymentInfo();

            _obj = (from d in _conn.Employees
                    join c in _conn.REC_CLIENT on d.client_id equals c.id
                    join e in _conn.REC_CLIENT on d.employer_id equals e.id
                    join b in _conn.Branches on d.Branch_ID equals b.Branch_ID
                    join dept in _conn.Departments on d.Department_ID equals dept.Dept_ID
                    join et in _conn.EmployeeTypes on d.EmpType_ID equals et.EmpType_ID
                    join s in _conn.Shifts on d.Shift_ID equals s.Shift_ID
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                  .Select(x => new EmploymentInfo()
                    {
                        EmployerName        = x.REC_CLIENT1.client_name,
                        ClientName          = x.REC_CLIENT.client_name,
                        Branch              = x.Branch.Branch_Desc,
                        Department          = x.Department.Dept_Name,
                        Position            = x.Position,
                        EmployeeType        = x.EmployeeType.EmpType_Desc,
                        ShiftSched          = x.Shift.Description,
                        DateHired           = DateTime.Parse(x.Datehired.Value.ToShortDateString())
                  }).SingleOrDefault();

            return _obj;
        }

        public EmployeeEducation GetEmployeeEducation(string GuId)
        {
            //get empid
            int _empid = _globalrepository.GetEmployeeKey(GuId).EmpId;
            EmployeeEducation _education = new EmployeeEducation
            {
                EducationalBackgroundList = GetEducation(_empid)
            };

            return _education;
        }

        public List<EducationalBackgroundViewModel> GetEducation(int _empid)
        {

            List<EducationalBackgroundViewModel> _obj = new List<EducationalBackgroundViewModel>();

            _obj = (from d in _conn.REC_CANDIDATE_EDUCATION
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.candidate_id equals l.candidate_id
                    join sl in _conn.SchoolLevels on d.SchoolLevelID equals sl.LevelID
                    join dg in _conn.Degrees on d.DegreeID equals dg.DegreeID
                    where l.emp_id == _empid
                    select d).AsEnumerable()
                  .Select(x => new EducationalBackgroundViewModel()
                  {
                      Level         = x.SchoolLevel.SchoolLevelDescn,
                      SchoolName    = x.School.SchoolName,
                      Degree        = x.Degree.DegreeName,
                      Period        = x.School_From.Value.Year + "-" + x.School_To.Value.Year,
                      Graduate      = x.Graduated.Value == true ? "Graduate" : "Under Graduate"
                  }).ToList();

            return _obj;
        }

        //public List<PreviousEmploymentViewModel> GetPreviousEmployment(int _empid)
        //{

        //    List<PreviousEmploymentViewModel> _obj = new List<PreviousEmploymentViewModel>();

        //    _obj = (from d in _conn.REC_CANDIDATE_EDUCATION
        //            join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.candidate_id equals l.candidate_id
        //            join sl in _conn.SchoolLevels on d.SchoolLevelID equals sl.LevelID
        //            join dg in _conn.Degrees on d.DegreeID equals dg.DegreeID
        //            where l.emp_id == _empid
        //            select d).AsEnumerable()
        //          .Select(x => new PreviousEmploymentViewModel()
        //          {

        //          }).ToList();

        //    return _obj;
        //}
    }
}