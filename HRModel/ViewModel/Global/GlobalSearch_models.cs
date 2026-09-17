using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class GlobalSearch_models
    {
        public class EmployeeSearchList
        {
            //public EmployeeSearchList();

            public string Branch { get; set; }
            public int BranchID { get; set; }
            public string ClientName { get; set; }
            public string ContractType { get; set; }
            public DateTime DateEncoded { get; set; }
            public DateTime Datehired { get; set; }
            public string Department { get; set; }
            public int EmpID { get; set; }
            public int EmployerId { get; set; }
            public string EmployerName { get; set; }
            public string EmpNo { get; set; }
            public int EmpStatus { get; set; }
            public string FirstName { get; set; }
            public bool IsIncludePayroll { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Paytype { get; set; }
            public string Position { get; set; }
            public int RankID { get; set; }
            public string SourceType { get; set; }
            public string UserEncoded { get; set; }
        }

        public class EmployeeTypeModel
        {
            public int EmpTypeID { get; set; }
            public string EmpTypeDesc { get; set; }
            public int UserID { get; set; }
            public bool Status { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class EmployeeRankModel
        {
            public int EmpRankID { get; set; }
            public string EmployeeRank { get; set; }
            public int UserID { get; set; }
            public bool Status { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class DepartmentModel
        {
            public int DeptID { get; set; }
            public string DeptName { get; set; }
        }

        public class BranchModel
        {
            public int BranchID { get; set; }
            public string BranchDesc { get; set; }
            public int ClientID { get; set; }
            public bool Status { get; set; }
        }

        public class ShiftModel
        {
            public int ShiftID { get; set; }
            public string Description { get; set; }
        }

        public class Contract
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }
            public DateTime DateHired { get; set; }
            public DateTime? ContractStart { get; set; }
            public DateTime? ContractEnd { get; set; }
            public DateTime? DateRegularized { get; set; }
            public string HireType { get; set; }
            public int EmployeeTypeId { get; set; }
            public int EmployeeRankId { get; set; }
            public string Position { get; set; }
            public bool CurrentContract { get; set; }
            public int HiredById { get; set; }
            public string HiredBy { get; set; }
            public int JoDetId { get; set; }
            public string DateCreated { get; set; }
            public int DepartmentId { get; set; }
            public int BranchId { get; set; }
            public int ShiftId { get; set; }
            public int RestDayId { get; set; }
            public string IsContractExtended { get; set; }
            public string DateExtended { get; set; }

            public int mode { get; set; }
        }
    }
}
