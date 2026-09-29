using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Client
{
    public class Client_model
    {
        public class ClientMonitoring_model
        {
            public int Id { get; set; }
            public string ClientGUID { get; set; }
            public string ClientName { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNumber { get; set; }
            public string EmailAddress { get; set; }
            public string TINno { get; set; }
            public string Industry { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientProfile_model
        {
            public int Id { get; set; }
            public string ClientGUID { get; set; }
            public string ClientName { get; set; }
            public string ClientAddress { get; set; }
            public string BillingAddress { get; set; }
            public string ZIPCode { get; set; }
            public string TINno { get; set; }
            public bool IsVatable { get; set; }
            public string VATType { get; set; }
            public bool IsWithHoldingTax { get; set; }
            public decimal WHTRate { get; set; }

            public string ContactNo { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonTitle { get; set; }
            public string EmailAddress { get; set; }
            public string MobileNo { get; set; }
            public int IndustryId { get; set; }
            public string IndustryName { get; set; }

            public string Website { get; set; }
            public string ClientType { get; set; }

            public string Remarks { get; set; }
            public int EmployerId { get; set; }
            public string EmployerName { get; set; }


            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }

            public int Mode { get; set; }
        }

        //========================CLIENT DOCUMENT========================
        public class ClientDocument_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public DateTime? DateIssued { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public string Remarks { get; set; }
            public string FileLocation { get; set; }
            public string FileExtension { get; set; }
            public bool? IsSigned  { get; set; }
            public bool? IsOriginal { get; set; }
            public bool? IsNotarized { get; set; }
            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        public class ClientDocument_vw_model
        {
            public int Id { get; set; }
            public string DocumentType { get; set; }
            public string DateIssued { get; set; }
            public string ExpiryDate { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        //========================CLIENT DEPARTMENT========================
        public class ClientDepartment_vw_model
        {
            public int Id { get; set; }
            public string DepartmentCode { get; set; }
            public string DepartmentName { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientDepartment_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string DepartmentCode { get; set; }
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }
            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        //========================CLIENT BRANCH========================
        public class ClientBranch_vw_model
        {
            public int Id { get; set; }
            public string BranchName { get; set; }
            public decimal NoOfDays { get; set; }
            public string RegionNo { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientBranch_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string BranchName { get; set; }
            public decimal NoOfDays{ get; set; }
            public string RegionNo { get; set; }
            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        //========================CLIENT BANK========================
        public class ClientBank_vw_model
        {
            public int Id { get; set; }
            public string AccountNo { get; set; }
            public string BankName { get; set; }
            public string ContactPerson { get; set; }            
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientBank_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int BankId { get; set; }
            public string BankName { get; set; }
            public string AccountNo { get; set; }
            public string Address { get; set; }
            public string ContactPerson { get; set; }
            public string Status { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        //========================CLIENT CONTACTS========================
        public class ClientContact_vw_model
        {
            public int Id { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNo { get; set; }
            public string EmailAddress { get; set; }
            public string PositionTitle { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientContact_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNo { get; set; }
            public string EmailAddress { get; set; }
            public string PositionTitle { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        //========================CLIENT DEPLOYED EMPLOYEE========================
        public class ClientEmployee_vw_model
        {
            public int Id { get; set; }
            public string EmployeeGUID { get; set; }
            public string EmpNo { get; set; }
            public string EmployeeName { get; set; }
            public string Position { get; set; }
            public string DepartmentName { get; set; }
            public string BranchName { get; set; }
            public string ContractType { get; set; }
            public string SourceType { get; set; }
            public string PayType { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        //========================CLIENT ADJUSTMENT========================
        public class ClientAdjustment_vw_model
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Classification { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientAdjustment_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string Description { get; set; }
            public int ClassificationId { get; set; }
            public string Classification { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }
        
        //========================CLIENT DEDUCTION========================
        public class ClientDeduction_vw_model
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientDeduction_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

        //========================CLIENT SHIFT SCHEDULE========================
        public class ClientShift_vw_model
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class ClientShift_model
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int ShiftId { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public int Mode { get; set; }
        }

 

       
    }


}
