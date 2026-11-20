using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class AccessModel
    {
        public class AppMenu
        {
            public string MenuType { get; set; }
            public int SortNo { get; set; }
            public string ControllerName { get; set; }
            public string Actionname { get; set; }
            public string MenuName { get; set; }
            public string ClassName { get; set; }
            public string ImageClassName { get; set; }
            public List<SubMenu> SubMenus { get; set; }
        }

        public class SubMenu
        {
            public int SortNo { get; set; }
            public string ControllerName { get; set; }
            public string Actionname { get; set; }
            public string MenuName { get; set; }
            public string ClassName { get; set; }
            public string ImageClassName { get; set; }
        }

        public static class Feature
        {
            public const string Add = "ADD";
            public const string Edit = "EDIT";
            public const string Delete = "DELETE";
            public const string Print = "PRINT";
            public const string System = "SYSTEM";
            public const string Post = "POST";
            public const string Unpost = "UNPOST";
            public const string Void = "VOID";
        }

        public static class SystemModuleType
        {
            public const int Masterfile = 1;
            public const int Transaction = 2;
            public const int Inquiry = 3;
            public const int Report = 4;
            public const int System = 5;
            public const int Process = 6;
        }

        public class ModuleAccess_model
        {
            public string ModuleName { get; set; }
            public int ModuleTypeId { get; set; }
            public int UserId { get; set; }
            public string UserType { get; set; }
            public int AppId { get; set; }
        }

        public class FunctionAccess_model
        {
            public string ModuleName { get; set; }
            public string FunctionAction { get; set; }
            public int ModuleTypeId { get; set; }
            public int UserId { get; set; }
            public string UserType { get; set; }
            public int AppId { get; set; }
            public string AppName { get; set; }
        }

        public class AvailableModule_model
        {
            public int Id { get; set; }
            public string ModuleName { get; set; }
        }

        public class AccessableModule_model
        {
            public int Id { get; set; }
            public int ModuleId { get; set; }
            public string ModuleName { get; set; }
            public bool AddAccess { get; set; }
            public bool EditAccess { get; set; }
            public bool DeleteAccess { get; set; }
            public bool PrintAccess { get; set; }
            public bool PostAccess { get; set; }
            public bool UnpostAccess { get; set; }
            public bool CancelAccess { get; set; }
            public bool ActivateAccess { get; set; }
            public bool DeactivateAccess { get; set; }
            public string ProvidedBy { get; set; }
            public DateTime DateProvided { get; set; }

            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public static class ModuleName
        {
            public static class HRMS
            {
                public static class MasterFile
                {
                    public const string Department = "Department Master File";
                    public const string EmployeeType = "Employee Type Master File";
                    public const string EmployeeRank = "Employee Rank Master File";
                    public const string Holiday= "Holiday Master File";
                    public const string SalaryType = "Salary Type Master File";
                    public const string Shift = "Shift Master File";
                    public const string Document = "Document Master File";
                
                    public const string EmployeeInformation = "Employee Information";
                    public const string EmployeeDocument = "Employee Document";
                    public const string EmployeeEducation = "Employee Education";
                    public const string EmployeeSkill = "Employee Skill";
                    public const string EmployeeSpouse = "Employee Spouse";
                    public const string EmployeeEmergencyContact = "Employee Emergency Contact";
                    public const string EmployeeExternalEmployment = "Employee External Employment History";
                    public const string EmployeeInternalEmployment = "Employee Internal Employment History";
                    public const string ClientMasterList = "Client Master List";
                    public const string ClientMasterfile = "Client Master File";
                    public const string ClientDocuments = "Client Documents";
                    public const string ClientDepartments = "Client Departments";
                    public const string ClientBranches = "Client Branches";
                    public const string ClientBanks = "Client Banks";
                    public const string ClientContacts = "Client Contacts";
                    public const string ClientAdjustments = "Client Adjustments";
                    public const string ClientDeductions = "Client Deductions";
                    public const string ClientSchedules = "Client Schedules";
                    public const string ClientAccountMapping= "Client Account Mapping";
                    public const string DepartmentMasterFile = "Department Master File";
                }


                public static class Transaction
                {
                    public const string ContractManagement = "Employee Transaction Monitoring";
                    public const string EmployeePayrollInfo = "Employee Payroll Info"; 
                    public const string EmployeePayrollAdjustments = "Employee Payroll Deduction";
                    public const string EmployeePayrollRemarks = "Employee Payroll Remarks";
                    public const string EmployeePayrollAdjustment = "Employee Payroll Adjustment";
                    public const string EmployeePayrollDeduction = "Employee Payroll Deduction";
                    public const string EmployeeLoan = "Employee Loan";
                    public const string EmployeeBenefits = "Employee Benefits";
                    public const string EmployeeTransactionMonitoring= "Employee Transaction Monitoring";
                    public const string ContractRemarks = "Contract Remarks";
                    public const string PersonnelMovementDetails = "Personnel Movement Details";
                    public const string EmployeeFiledLeave = "Employee Filed Leave Monitoring";
                    public const string ReActivationEmployeeMonitoring = "Re-Activate Employee Monitoring";
                    public const string BankDetailsBatchUploading = "Bank Details Batch Uploading";
                    public const string SalaryDetailsBatchUploading = "Salary Details Batch Uploading";
                    public const string PortalAccountGenerator = "Portal Account Generator";
                }

                public static class Inquiry
                {
                    public const string EmployeeMonitoring = "Employee Monitoring";
                    public const string ClientJobOrderHistory = "Client Job Order History";
                    public const string ClientPayrollHistory = "Client Payroll History";
                    public const string ClientInvoiceHistory = "Client Invoice History";
                    public const string ClientCreditMemoHistory = "Client Credit Memo History";
                    public const string ClientOfficialReceiptHistory= "Client Official Receipt History";
                    public const string ClientPayrollSetup = "Client Payroll Setup";
                    public const string ClientInterimSetup = "Client Interim Setup";
                    public const string ClientOvertimeSetup = "Client Overtime Setup";
                    public const string ClientPayrollServicesSetup = "Client Payroll Services Setup";
                    public const string ClientPortalRulesSetup = "Client Portal Rules Setup";
                    public const string EmployeeNotesMonitoring = "Employee Notes Monitoring";
                    public const string PersonnelMovementMonitoring = "Personnel Movement Monitoring";
                    public const string LoanMonitoring = "Loan Monitoring";
                    public const string EmployeeLeaveBalanceMonitoring = "Employee Leave Balance Monitoring";
                    public const string RequiredDocumentMonitoring = "Required Document Monitoring";
                }

                public static class System
                {
                    public const string EmployeePortalAccount = "Employee Portal Account";
                    public const string EmployeePortalResetPassword= "Employee Portal Reset Password";
                    public const string EmployeePortalUpdateEmail = "Employee Portal Update Email";
                    public const string ClientPayrollSetupSave = "Client Payroll Setup Save";
                    public const string ClientInterimSetupSave = "Client Interim Setup Save";
                    public const string ClientOvertimeSetupSave = "Client Overtime Setup Save";
                    public const string ClientPayrollServicesSetupSave = "Client Payroll Services Setup Save";
                    public const string ClientPortalRulesSetupSave = "Client Portal Rules Setup Save";
                    public const string AccessMatrixCopyAccess = "Access Matrix Copy Access";
                    public const string AccessMatrix = "Access Matrix";
                    public const string AccessMatrixSave = "Access Matrix Save";
                    public const string ClientPortalAccess = "Client Portal Access";
                    public const string PortalModuleAccess = "Portal Module Access";
                    public const string ApprovedLeave= "Approved Leave";
                    public const string RevokedLeave = "Revoked Leave";
                }

                public static class Process
                {
                    public const string TaggingofFloatingemployee = "Tagging of Floating Employee";
                    public const string EmployeeContractExtension = "Employee Contract Extension";
                    public const string EmployeeBenefitsUploading = "Employee Benefits Uploading";
                }
            }
        }
    }
}
