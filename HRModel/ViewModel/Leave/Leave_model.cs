using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Leave
{
    public class Leave_model
    {
        public class LeaveModel
        {
            [Required(ErrorMessage = "Id is as required field.")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Emp Id is as required field.")]
            public int EmpId { get; set; }
            public string EmpName { get; set; }
            public string ClientName { get; set; }

            [Required(ErrorMessage = "Leave type is as required field.")]
            public int LeaveTypeId { get; set; }
            public bool EmergencyLeave { get; set; }
            public DateTime DateFiled { get; set; }

            [Required(ErrorMessage = "Leave from is as required field.")]
            public DateTime LeaveFrom { get; set; }

            public string LeaveFromAMPM { get; set; }

            [Required(ErrorMessage = "Leave to is as required field.")]
            public DateTime LeaveTo { get; set; }

            public string LeaveToAMPM { get; set; }

            [Required(ErrorMessage = "Leave days is as required field.")]
            public decimal LeaveDays { get; set; }
            public bool IsHalfday { get; set; }
            public bool FirstHalf { get; set; }
            public bool SecondHalf { get; set; }
            public bool FirstDay_SecondHalf { get; set; }
            public bool LastDay_FirstHalf { get; set; }

            [Required(ErrorMessage = "Reason is as required field.")]
            public string Reason { get; set; }
            public string Remarks { get; set; }
            public int Mode { get; set; }
            public string Message { get; set; }
            public int UserId { get; set; }

            public string FileExtension { get; set; }
            public string HasAttachement { get; set; }
            public string guid { get; set; }
            public int DTRId { get; set; }
            public string ShiftDescription { get; set; }
            public string LeaveFromStr { get; set; }
            public string LeaveToStr { get; set; }

            public decimal EntitledLeave { get; set; }
            public decimal EarnedPerMonth { get; set; }
            public decimal AvailableLeave { get; set; }
            public decimal UsedLeave { get; set; }
            public decimal BalanceLeave { get; set; }
            public int YearEntitled { get; set; }
            public int MonthEntitled { get; set; }

            public string LeaveType { get; set; }
            public string DynamicRuleMessage { get; set; }

            public bool? Status { get; set; }
            public int FileStatus { get; set; }
            public bool IsRejected { get; set; }
            public bool IsCancel { get; set; }

            public LeaveModel()
            {
                Id = 0;
                EmpId = 0;
                EmpName = "";
                ClientName = "";
                LeaveTypeId = 1;
                EmergencyLeave = false;
                DateFiled = DateTime.Now;
                LeaveFrom = DateTime.Now;
                LeaveFromAMPM = "AM";
                LeaveTo = DateTime.Now;
                LeaveToAMPM = "PM";
                IsHalfday = false;
                FirstHalf = true;
                SecondHalf = false;
                Reason = "";
                FirstDay_SecondHalf = false;
                LastDay_FirstHalf = false;
                Reason = "";
                Remarks = "";
                Mode = 0;
                Message = "";
                UserId = 0;

                FileExtension = "";
                HasAttachement = "Hidden";
                guid = "";
                DTRId = 0;
                ShiftDescription = "";
            }

            public string LeaveTypeDescription
            {
                get
                {
                    switch (this.LeaveTypeId)
                    {
                        case 1: return "Sick Leave";
                        case 2: return "Vacation Leave";
                        case 3: return "Emergency Leave";
                        case 4: return "Maternity Leave";
                        case 5: return "Paternity Leave";
                        default: return "Unknown";
                    }
                }
            }
        }

        public class LeaveBalanceModel
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }
            public int LeaveTypeId { get; set; }
            public decimal EntitleLeave { get; set; }
            public decimal RemainingLeave { get; set; }
            public decimal UsedLeave { get; set; }
            public int YearEntitled { get; set; }
            public int MonthEntitled { get; set; }
            
            public DateTime? ValidFrom { get; set; }
            public DateTime? ValidTo { get; set; }

            public DateTime? ValidDateFrom { get; set; }
            public DateTime? ValidDateTo { get; set; }

            public int UserId { get; set; }
            public DateTime DateAdded { get; set; }
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public decimal CreditEarnedPerMonth { get; set; }
            public bool IsConvertable { get; set; }
            public bool AutoResetPerYear { get; set; }
            public string WhenCreditIsEarned { get; set; }
            public decimal EarnedLeave { get; set; }
            public decimal BalanceLeave { get; set; }

            public int? Mode { get; set; }
        }

        public class EmployeeFiledLeave
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public string DateFiled { get; set; }
            public string LeaveType { get; set; }
            public string LeaveDays { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public string Reason { get; set; }
            public string LeaveFrom { get; set; }
            public string LeaveTo { get; set; }
        }

        public class LeaveFilter_model
        {
            public int ClientId { get; set; } 
            public string  Status { get; set; }
            public bool ByMonthYear { get; set; }
            public int MonthLeave { get; set; }
            public int YearLeave { get; set; }
            public bool ByDate { get; set; } 
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public string keyword { get; set; }
            public int CompanyId { get; set; }

        }

        public class LeaveTypeModel
        {
            public int LeaveTypeId { get; set; }
            public string LeaveTypeDesc { get; set; }
            public string LeaveCode { get; set; }
        }

        public class CoorLeaveBalanceMonitoringModel
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string EmployeeName { get; set; }
            public string LeaveType { get; set; }
            public decimal EntitledLeave { get; set; }
            public decimal EarnedPerMonth { get; set; }
            public decimal AvailableLeave { get; set; }
            public decimal UsedLeave { get; set; }
            public decimal BalanceLeave { get; set; }
            public string AddedBy { get; set; }
            public string DateAdded { get; set; }
            public string Status { get; set; }
            public int YearEntitled { get; set; }
            public int MonthEntitled { get; set; }
            public int ClientId { get; set; }
            public int EmpId { get; set; }
        }

        public class EmployeeLeaveDetailMonitoring_model
        {
            public int Id { get; set; }
            public int EmpId { get; set; }
            public string EmpNo { get; set; }
            public string EmployeeName { get; set; }
            public string ClientName { get; set; }
            public string DepartmentName { get; set; }

            public int LeaveTypeId { get; set; }
            public string LeaveType { get; set; }

            public decimal EntitleLeave { get; set; }
            public decimal RemainingLeave { get; set; }
            public decimal UsedLeave { get; set; }
            public decimal BalanceLeave { get; set; }
            public decimal EarnedLeave { get; set; } 

            public int MonthEntitled { get; set; }
            public int YearEntitled { get; set; }
            public string EntitledPeriod { get; set; }

            public DateTime ValidDateFrom { get; set; }
            public DateTime? ValidDateTo { get; set; }
            
            public DateTime ValidFrom
            {
                get => ValidDateFrom;
                set => ValidDateFrom = value;
            }
            public DateTime? ValidTo
            {
                get => ValidDateTo;
                set => ValidDateTo = value;
            }

            public int UserId { get; set; }
            public int ClientId { get; set; }
            public bool Status { get; set; }
            public DateTime DateAdded { get; set; }
            public decimal CreditEarnedPerMonth { get; set; }
            public bool IsConvertable { get; set; }
            public bool AutoResetPerYear { get; set; }
            public string WhenCreditIsEarned { get; set; }

            public string Mode { get; set; }
        }

        public class LeaveAcceptModel
        {
            public int Id { get; set; }
            public int PLeaveId { get; set; }
            public DateTime DateAccepted { get; set; }
            public string Remarks { get; set; }
            public int Mode { get; set; }
            public int UserId { get; set; }
        }
    }
}
