using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class MasterFile
    {
        public class DepartmentModel
        {
            public class Department_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class Department_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class EmployeeTypeModel
        {
            public class EmployeeType_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class EmployeeType_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class EmployeeRankModel
        {
            public class EmployeeRank_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class EmployeeRank_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class SalaryTypeModel
        {
            public class SalaryType_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class SalaryType_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class ShiftModel
        {
            public class Shift_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public DateTime ShiftIn { get; set; }
                public DateTime ShiftOut { get; set; }
                public int GraceMinute { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class Shift_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public DateTime ShiftIn { get; set; }
                public DateTime ShiftOut { get; set; }
                public int GraceMinute { get; set; }
                public DateTime BreakTimeStart { get; set; }
                public DateTime BreakTimeEnd { get; set; }
                public bool IsGraveYard { get; set; }
                public int CountryId { get; set; }

                public DateTime? GraveTimeIn { get; set; }
                public DateTime? GraveTimeOut { get; set; }

                public bool IsHalfDay { get; set; }
                public bool IsCompress { get; set; }
                public decimal HoursWork { get; set; }

                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class BankTypeModel
        {
            public class BankType_list_model
            {
                public int Id { get; set; }
                public string BankCode { get; set; }
                public string BankDescription { get; set; }
                public string Status { get; set; }

                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }
        }

        public class DocumentTypeModel
        {
            public class DocumentType_list_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string DocumentClass { get; set; }
                public string Status { get; set; }
                public string CreatedBy { get; set; }
                public string DateCreated { get; set; }
            }

            public class DocumentType_model
            {
                public int Id { get; set; }
                public string Description { get; set; }
                public string DocumentClass { get; set; }
                public bool IsAccessibleOutside { get; set; }
                public int CountryId { get; set; }
                public bool Status { get; set; }
                public int UserId { get; set; }
                public string CreatedBy { get; set; }
                public int Mode { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }

        public class LocalHoliday_Input_model
        {
            public int Id { get; set; } = 0;
            public DateTime HolidayDate { get; set; }
            public string HolidayDescription { get; set; }
            public string HolidayType { get; set; }
            public int Mode { get; set; }
            public int UserId { get; set; } = 0;
        }

        public class LocalHolidayDetail_Input_model
        {
            public int Id { get; set; } = 0;
            public int HolidayId { get; set; }
            public int EmpId { get; set; }
            public int Mode { get; set; } = 0;
            public int UserId { get; set; }
        }

        public class HolidayList_model
        {
            public int HolidayId { get; set; }
            public string HolidayDescription { get; set; }
            public DateTime? HolidayDate { get; set; }
            public int? UserId { get; set; }
            public string HolidayType { get; set; }
            public string HolidayTypeDesc { get; set; }
            public bool WorkingHoliday { get; set; }
            public DateTime? DateCreated { get; set; }

            public int AssignedCount { get; set; }
        }

        public class LocalHolidayDetailViewModel
        {
            public int Id { get; set; }
            public int HolidayId { get; set; }
            public int EmpId { get; set; }
            public int UserId { get; set; }
            public DateTime DateCreated { get; set; }

            public string EmployeeName { get; set; }
            public string EmpNo { get; set; }
        }

        public class ModuleType_model
        {
            public int Id { get; set; }
            public string ModuleType { get; set; }
        }

    }
}
