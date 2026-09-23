using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Contract.EmployeeTransaction
{
    public class EmployeeTransaction
    {
        public class Monitoring
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public string HireType { get; set; }
            public string DateHired { get; set; }
            public string ContractStart { get; set; }
            public string ContractEnd { get; set; }
            public string HiredBy { get; set; }
            public int JoDetId { get; set; }

            public string DateResignationSubmitted { get; set; }
            public string DateSeparated { get; set; }
            public string DateInactive { get; set; }
            public string DateInactiveCreated { get; set; }
            public int FloatingId { get; set; }
            public int SeparationId { get; set; }
            public int UserId { get; set; }
            public string ContractStatus { get; set; }
            public string EmployeeType { get; set; }
            public string Position { get; set; }
            public string DateRegularized { get; set; }
            public int ClientId { get; set; }
            public string ContractExtended { get; set; }
        }

        public class ClientList
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string ClientAddress { get; set; }
            public string ContactNo { get; set; }
            public string EmailAddress { get; set; }
            public string ContactPerson { get; set; }
            public string ContactTitle { get; set; }
            public string MobileNo { get; set; }
            public bool? Status { get; set; }
            public string TinNo { get; set; }
            public int IndustryId { get; set; }
            public DateTime? DateEncoded { get; set; }
            public string Website { get; set; }
            public int UserId { get; set; }
            public bool Vatable { get; set; }
            public string ClientType { get; set; }
            public string ZipCode { get; set; }
            public string SssNo { get; set; }
            public string PagibigNo { get; set; }
            public string PhilhealthNo { get; set; }
            public string CompanyType { get; set; }
            public bool IsWithholdingTax { get; set; }
            public decimal WithholdingTaxRate { get; set; }
            public string VatType { get; set; }
            public string BillingAddress { get; set; }
            public int EmployerId { get; set; }
            public string Guid { get; set; }
        }

        public class EmployeeRehire_model
        {
            public int EmpId { get; set; }
            public int EmployerId { get; set; }
            public int CompanyId { get; set; }
            public int ClientId { get; set; }
            public int BranchId { get; set; }
            public int ShiftId { get; set; }
            public int DepartmentId { get; set; }
            public int EmployeeRankId { get; set; }
            public int EmployeeTypeId { get; set; }
            public string Position { get; set; }
            public int FunctionId { get; set; }
            public string PayType { get; set; }
            public int SalaryTypeId { get; set; }
            public decimal MonthlySalary { get; set; }
            public bool IsMinumum { get; set; }
            public DateTime ContractStart { get; set; }
            public DateTime ContractEnd { get; set; }
            public int UserId { get; set; }
        }


        public class ContractRemarks
        {
            public int Id { get; set; }
            public int TranId { get; set; }
            public DateTime DateCreated { get; set; }
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Remarks { get; set; }
            public int Mode { get; set; }
        }
    }
}
