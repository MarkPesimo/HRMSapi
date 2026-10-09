using System;
using System.Collections.Generic;

namespace HRModel.ViewModel.Employees
{
    public class PersonalInfo
    {
        public int EmpId { get; set; }
        public string EmployeeNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string EmailAdd { get; set; }
        public string ContactNo { get; set; }
        public string PresentAdd { get; set; }
        public string City { get; set; }
        public string ProvincialAdd { get; set; }
        public string Province { get; set; }    
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public string Hobbies { get; set; }
        public int Mode { get; set; }
        public int UserId { get; set; }
    }

    public class AreaLibraryModel
    {
        public int AreaID { get; set; }
        public string AreaDescription { get; set; }
        public int? ProvinceID { get; set; }
        public int? UserID { get; set; }
        public int? user_id { get; set; }
        public DateTime? date_created { get; set; }
        public bool? status { get; set; }
        public int? region_id { get; set; }
    }

    public class SpouseInfo
    {
        public int EmpId { get; set; }
        public string SpouseName { get; set; }
        public string SpouseCompany { get; set; }
        public string SpouseCompanyAdd { get; set; }
        public int UserId { get; set; }
        public int Mode { get; set; }
    }

    public class EmergencyContactInfo
    {
        public int EmpId { get; set; }
        public string ContactPerson { get; set; }
        public string ContactRelation { get; set; }
        public string ContactNo { get; set; }
        public string ContactAdd { get; set; }
        public int UserId { get; set; }
        public int Mode { get; set; }
    }

    public class GovernmentNos
    {
        public int EmpId { get; set; }
        public string SSSNo { get; set; }
        public string PhilhealthNo { get; set; }
        public string PagibigNo { get; set; }
        public string TINNo { get; set; }

        public int UserId { get; set; }
        public int Mode { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }

    public class EmploymentInfo
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmployerName { get; set; }
        public string ClientName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeRank { get; set; }
        public string ShiftSched { get; set; }
        public string Function { get; set; }
        public string SalaryType { get; set; }
        public DateTime DateHired { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public DateTime DateRegular { get; set; }
        public int EmployerId { get; set; }
        public int ClientId { get; set; }
        public int BranchId { get; set; }
        public int DeptId { get; set; }
        public int EmpTypeId { get; set; }
        public int EmpRankId { get; set; }
        public int ShiftId { get; set; }
        public int SalaryTypeId { get; set; }
        public int FunctionId { get; set; }
    }


    //==============================EMPLOYEE EDUCATION=========================================
    public class EducationalBackgroundViewModel
    {
        public int SchoolId { get; set; }
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string Level { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string Period { get; set; }
        public string Graduate { get; set; }
    }

    public class EducationalBackgroundModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }

        public int SchoolLevelId { get; set; }
        public string SchoolLevel { get; set; }

        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
        public string Awards { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Remarks { get; set; }
        public bool IsGraduated { get; set; }

        public int UserId { get; set; }
        public int Mode { get; set; }

    }

    public class SchoolViewModel
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolType { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; }
        public int? RegionId { get; set; }
        public int? SchoolTierId { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CountryId { get; set; }
        public int Mode { get; set; }
        public int UserId { get; set; }
    }

    public class DegreeViewModel
    {
        public int DegreeID { get; set; }
        public string DegreeName { get; set; }
        public bool Status { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CountryId { get; set; }
        public int Mode { get; set; }
        public int UserId { get; set; }
    }

    public class SchoolLevelViewModel
    {
        public int LevelID { get; set; }
        public string SchoolLevelDescn { get; set; }
        public bool? Status { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CountryId { get; set; }
    }
    //==============================EMPLOYEE EDUCATION=========================================

    //==============================EMPLOYEE SKILL=========================================
    public class SkillViewModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string SkillName { get; set; }
        public string Proficiency { get; set; }
        public int YearsOfExperience { get; set; }
        public string Remarks { get; set; }
    }

    public class SkillModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public int UserId { get; set; }
        public int Mode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
                                   
        //public string SkillLevel { get; set; }
                                           //public int YearsOfExperience { get; set; }
                                           //public string Remarks { get; set; }

        //public int SkillLevelId { get; set; }
    }
    //==============================EMPLOYEE SKILL=========================================

    //==============================INTERNAL EMPLOYMENT=========================================
    public class InternalEmploymentViewModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string ClientName { get; set; }
        public string Position { get; set; }
        public string HireType { get; set; }
        public string EmployeeType { get; set; }
        public string ContractStart { get; set; }
        public string ContractEnd { get; set; }
        public string SeparationDate { get; set; }
        public string InactiveDate { get; set; }
        public string ContractStatus { get; set; }
    }

    public class InternalEmploymentModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmployerName { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal SalaryAmount { get; set; }
        public string Reason { get; set; }
        public int EmployerId { get; set; }
        public int ClientId { get; set; }
        public int DeptId { get; set; }
        public int BranchId { get; set; }
        public int ReasonId { get; set; }
        public int SourceTypeId { get; set; }
        public int SeparationId { get; set; }
    }
    //==============================INTERNAL EMPLOYMENT=========================================

    //==============================EXTERNAL EMPLOYMENT=========================================
    public class PreviousEmploymentViewModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Industry { get; set; }
        public string Function { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public string EmploymentRank { get; set; }
        public string EmploymentPeriod { get; set; }
    }

    public class PreviousEmploymentModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public decimal? Salary { get; set; }

        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public string Function { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public string EmploymentRank { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmploymentTypeId { get; set; }
        
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ReasonForLeaving { get; set; }

        public string JobDescription { get; set; }
        public int UserId { get; set; }
        public int Mode { get; set; }
    }
    //==============================EXTERNAL EMPLOYMENT=========================================


    //==============================EMPLOYEE DOCUMENT=========================================
    public class EmployeeDocumentViewModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string ReferenceNo { get; set; }
        public string DateIssued { get; set; }
        public string FileLocation { get; set; }
        public int DocId { get; set; }
    }

    public class EmployeeDocumentModel
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? DateIssued { get; set; }
        public string FileLocation { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public int Mode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
    //==============================EMPLOYEE DOCUMENT=========================================

    //==============================EMPLOYEE PROFILE=========================================
    public class EmployeeProfile
    {
        public int CandId { get; set; }
        public string GUid { get; set; }
        public string EmployeeStatus { get; set; }
        public PersonalInfo Personal { get; set; }
        public SpouseInfo Spouse { get; set; }
        public EmergencyContactInfo EmergencyContact { get; set; }
        public EmploymentInfo CurrentEmployment { get; set; }
        public GovernmentNos GMBNos { get; set; }
        public List<EducationalBackgroundViewModel> EducationalBackgroundList { get; set; }
        public List<EmployeeDocumentViewModel> DocumentList { get; set; }
        public List<SkillViewModel> SkillList { get; set; }
        public List<PreviousEmploymentViewModel> ExternalEmployments { get; set; }
        //public List<>
    }
    //==============================EMPLOYEE PROFILE=========================================


    //==============================EMPLOYEE PAYROLL=========================================

    //------------------------------SALARY REMARKS----------------------------------------
    public class EmployeeSalary
    {
        public int EmpId { get; set; }
        public decimal? BasicRate { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Cola { get; set; }
        public decimal? RiceAllowance { get; set; }
        public decimal? Deminimis { get; set; }

        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string CardNo { get; set; }
        public DateTime? CardValidity { get; set; }

        public bool IsManualPagibig { get; set; }
        public decimal PagIbigContribution { get; set; }

        public bool  IsManualSSS { get; set; }
        public decimal SSSContribution { get; set; }

        public bool IsManualPhilhealth { get; set; }
        public decimal PhilhealthContribution { get; set; }

        public bool IsManaulTax { get; set; }   
        public decimal ManualTaxDeduction { get; set; }

        public bool? IncludeInPayroll { get; set; }
        public bool MinimumWageEarner { get; set; }

        public bool IsConfidential { get; set; }

        public bool IsWithVat { get; set; }
        public decimal? VatPercentage { get; set; }

        public int ContractTypeId { get; set; }
        public string ContractType { get; set; }

        public int SourceTypeId { get; set; }
        public string SourceType { get; set; }

        public string PayrollType { get; set; }
        public string Remarks { get; set; }

        public int RestDayId { get; set; }
        public string RestDayDesription { get; set; }

        public int CreatedByUserId { get; set; }

        public string CreatedByUser { get; set; }
        public DateTime DateCreated { get; set; }

        public int ModifiedByUserId { get; set; }
        public string ModifiedByUser { get; set; }
        public DateTime DateModified { get; set; }

        public int Mode { get; set; }
    }

    public class EmployeeSalaryRemark
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int RemarksTypeId { get; set; }
        public string RemarksType { get; set; }
        public string Remarks { get; set; }

        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        
        public int Mode { get; set; }
    }
    //------------------------------SALARY REMARKS----------------------------------------

    //------------------------------PAYROLL ADJUSTMENT----------------------------------------
    public class EmployeeAdjustment
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? AdjustmentId { get; set; }
        public string AdjustmentDescription { get; set; }
        public DateTime? TranDate { get; set; }
        public decimal? Amount { get; set; }

        public bool? IsTaxable { get; set; }
        public bool? Billable { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public int? UserId { get; set; }
        public string CreatedBy { get; set; }
        

        public int Mode { get; set; }

    }
    //------------------------------PAYROLL ADJUSTMENT----------------------------------------

    //------------------------------PAYROLL DEDUCTIONS----------------------------------------
    public class EmployeeDeduction
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? DeductionId { get; set; }
        public string DeductionDescription { get; set; }
        public DateTime? TranDate { get; set; }
        public decimal DayAbsent { get; set; }

        public decimal? Amount { get; set; }

        
        public string Remarks { get; set; }
        public string Status { get; set; }

        public int? UserId { get; set; }
        public string CreatedBy { get; set; }
        public decimal  Balance { get; set; }

        public int Mode { get; set; }

    }
    //------------------------------PAYROLL DEDUCTIONS----------------------------------------

    //==============================EMPLOYEE PAYROLL=========================================



    public class EmployeeEducation
    {
        public List<EducationalBackgroundViewModel> EducationalBackgroundList { get; set; }
        //public EducationalBackgroundModel EducationalBackgroundInfo { get; set; }
    }

    public class PreviousEmployment
    {
        public List<PreviousEmploymentViewModel> PreviousEmploymentList { get; set; }
        //public PreviousEmploymentModel PreviousEmploymentInfo { get; set; }
    }

    public class EmployeeSkills
    {
        public List<SkillViewModel> EmployeeSkillList { get; set; }
        public SkillModel EmployeeSkill { get; set; }
    }

    public class EmployeeDocument
    {
        public List<EmployeeDocumentViewModel> EmployeeDocumentsList { get; set; }
        public EmployeeDocumentModel EmployeeDocumentRecord { get; set; }

    }

    public class EmployeeKeys
    {
        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string EmployeeGUID { get; set; }
        public int CandidateId { get; set; }
    }

    public class LoanTypeModel
    {
        public int LoanTypeID { get; set; }
        public string LoanTypeDesc { get; set; }
        public int UserID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
