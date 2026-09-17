using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees
{
    public class PersonalInfo
    {
        public string EmployeeNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Nationality { get; set; }
        public string BirthPlace { get; set; }
        public string EmailAdd { get; set; }
        public string Skills { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PresentAdd { get; set; }
        public string ProvincialAdd { get; set; }
    }

    public class SpouseInfo
    {
        public int EmpID { get; set; }
        public string SpouseName { get; set; }
        public string SpouseCompany { get; set; }
        public string SpouseCompanyAdd { get; set; }
    }

    public class EmergencyContactInfo
    {
        public int EmpID { get; set; }
        public string ContactPerson { get; set; }
        public string ContactRelation { get; set; }
        public string ContactNo { get; set; }
        public string ContactAdd { get; set; }
    }

    public class EmploymentInfo
    {
        public int EmpID { get; set; }
        public string EmployerName { get; set; }
        public string ClientName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string EmployeeType { get; set; }
        public string ShiftSched { get; set; }
        public DateTime DateHired { get; set; }
    }

    public class EducationalBackgroundViewModel
    {
        public int EmpID { get; set; }
        public string Level { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string Period { get; set; }
        public bool Graduate { get; set; }
        public string EmployeeType { get; set; }
        public string ShiftSched { get; set; }
        public DateTime DateHired { get; set; }
    }

    public class EducationalBackgroundModel
    {
        public int EmpID { get; set; }        
        public string SchoolName { get; set; }
        public string Level { get; set; }
        public string Degree { get; set; }
        public string Awards { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Remarks { get; set; }
        public bool Graduate { get; set; }
    }

    public class SkillViewModel
    {
        public int EmpID { get; set; }
        public string SkillName { get; set; }
        public string Proficiency { get; set; }
        public int YearsOfExperience { get; set; }
        public string Remarks { get; set; }
    }

    public class SkillModel
    {
        public int EmpID { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public string Remarks { get; set; }
    }

    public class InternalEmploymentViewModel
    {
        public int EmpID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
    }

    public class PreviousEmploymentViewModel
    {
        public int EmpID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public string EmploymentPeriod { get; set; }
    }

    public class PreviousEmploymentModel
    {
        public int EmpID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class EmployeeProfile
    {
        public string GUid { get; set; }
        public PersonalInfo Personal { get; set; }
        public SpouseInfo Spouse { get; set; }
        public EmergencyContactInfo EmergencyContact { get; set; }
        public EmploymentInfo CurrentEmployment { get; set; }
        public List<EducationalBackgroundViewModel> EducationalBackgroundList { get; set; }
        public EducationalBackgroundModel EducationalBackgroundInfo { get; set; }
        public List<SkillViewModel> EmployeeSkillList { get; set; }
        public SkillModel EmployeeSkill { get; set; }
    }

    public class EmployeeKeys
    {
        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string EmployeeGUID { get; set; }
    }
}
