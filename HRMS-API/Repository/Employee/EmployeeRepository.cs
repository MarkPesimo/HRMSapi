using HRModel.ViewModel.Employees;
//using HRMS_API.Models;
using HRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.DB;
using System.Globalization;
using HRMS_API.Repository.Employee;

namespace HRMS_API.Repository
{
    public class EmployeeRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        private EmployeeEducationRepository _educationrepository { get; set; }
        private EmployeeDocumentRepository _documentrepository { get; set; }
        private EmployeeSkillRepository _skillrepository { get; set; }
        private EmployeeEmploymentRepository _employmentrepository { get; set; }

        public EmployeeRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_educationrepository == null) { _educationrepository = new EmployeeEducationRepository(); }
            if (_documentrepository == null) { _documentrepository = new EmployeeDocumentRepository(); }
            if (_skillrepository == null) { _skillrepository = new EmployeeSkillRepository(); }
            if (_employmentrepository == null) { _employmentrepository = new EmployeeEmploymentRepository(); }
        }

        public string GetEmployeeName(int _empid)
        {
            return (from d in _conn.Employees where d.Emp_ID == _empid select d.Lastname + ", " + d.Firstname).ToString();
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

        //=============================PROFILE / PERSONAL============================================
        public EmployeeProfile GetEmployeeProfile(string _guid, int _empid)
        {
            EmployeeProfile _profile = new EmployeeProfile
            {
                GUid = _guid,
                EmployeeStatus              = GetEmployeeStatus(_empid),
                Personal                    = GetPersonalInfo(_empid),
                Spouse                      = GetSpouseInfo(_empid),
                EmergencyContact            = GetEmergencyContact(_empid),
                CurrentEmployment           = GetCurrentEmployment(_empid),
                GMBNos                      = GetGovernmentInfo(_empid),
                EducationalBackgroundList   = _educationrepository.GetEducations(_empid),
                DocumentList                = _documentrepository.GetDocuments(_empid),
                SkillList                   = _skillrepository.GetSkills(_empid),
                ExternalEmployments         = _employmentrepository.GetExternalEmployments(_empid)
            };

            return _profile;
        }

        public string GetEmployeeStatus(int _empid)
        {
            bool? _status = (from d in _conn.Employees where d.Emp_ID == _empid select d.IsIncludePayroll).SingleOrDefault();
            if (bool.Parse(_status.ToString())) { return "Active"; }
            else { return "Inactive"; }
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
                        Hobbies          = "",
                        CityId          = x.CityID,
                        ProvinceId      = x.ProvinceID
                    }).SingleOrDefault();

            return _obj;
        }

        public int ManagePersonal(PersonalInfo _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_PERSONAL(
                _model.EmpId,
                _model.EmployeeNo,
                _model.FirstName,
                _model.LastName,
                _model.MiddleName,
                _model.BirthDate,
                _model.Gender,
                _model.CivilStatus,
                _model.Nationality,
                _model.BirthPlace,
                _model.EmailAdd,
                _model.Hobbies,
                _model.CityId,
                _model.ProvinceId,
                _model.PresentAdd,
                _model.ProvincialAdd,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //=============================PROFILE / PERSONAL============================================

        //=============================SPOUSE============================================
        public SpouseInfo GetSpouseInfo(int _empid)
        {
            return  (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select new SpouseInfo
                    {
                        SpouseName = d.Spouse,
                        SpouseCompany = d.Spouse_comp,
                        SpouseCompanyAdd = d.Spouse_address
                    }).SingleOrDefault();
        }

        public int ManageSpouse(SpouseInfo _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_SPOUSE(
                _model.EmpId,
                _model.SpouseName,
                _model.SpouseCompany,
                _model.SpouseCompanyAdd,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //=============================SPOUSE============================================

        //=============================EMERGENCY============================================
        public EmergencyContactInfo GetEmergencyContact(int _empid)
        {
            return (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select new EmergencyContactInfo
                    {
                        ContactPerson   = d.Contact_person,
                        ContactAdd      = d.Contact_Address,
                        ContactNo       = d.Contact_No,
                        ContactRelation = d.Contact_relation
                    }).SingleOrDefault();
        }

        public int ManageEmergencyContact(EmergencyContactInfo _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_EMERGENCY(
                _model.EmpId,
                _model.ContactPerson, 
                _model.ContactNo, 
                _model.ContactRelation,
                _model.ContactAdd,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //=============================EMERGENCY============================================

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


        //=============================GOVERNMENT============================================
        public GovernmentNos GetGovernmentInfo(int _id)
        {
            return (from x in _conn.Employees
                    where x.Emp_ID == _id
                    select x
                ).AsEnumerable()
                .Select(d => new GovernmentNos()
                {
                    EmpId = int.Parse(d.Emp_ID.ToString()),
                    SSSNo = d.SSS_no,
                    PhilhealthNo = d.Philhealth_no,
                    PagibigNo = d.Pagibig_no,
                    TINNo = d.Tax_no,
                    UserId = d.UserID,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_encoded
                }).SingleOrDefault();
        }

        public int ManageGovernmentInfo(GovernmentNos _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_MANAGE_EMPLOYEE_GOVERNMENT(
                _model.EmpId,
                _model.PhilhealthNo,
                _model.PagibigNo,
                _model.TINNo,
                _model.SSSNo,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //=============================GOVERNMENT============================================





    }
}