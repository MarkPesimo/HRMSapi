using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRMS_API.Repository.MasterFileRepository;

namespace HRMS_API.Repository.Employee
{
    public class EmployeePayrollRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private Bank_repository _bankrepository { get; set; }

        public EmployeePayrollRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_bankrepository == null) { _bankrepository = new Bank_repository(); }
        }



        //==============================SALARY REMARKS================================
        public List<EmployeeSalaryRemark> GetSalaryRemarks(int _empid)
        {

            return (from d in _conn.SALARY_INFO_REMARKS
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                  .Select(x => new EmployeeSalaryRemark()
                  {
                      Id = x.id,
                      EmpId = x.Emp_ID,
                      RemarksTypeId = x.RemarksType_ID,
                      RemarksType = x.SALARY_REMARKS_TYPE.Remarks_Type,
                      Remarks = x.Remarks,
                      UserId = x.UserID,
                      CreatedBy = x.SYS_USER.username,
                      DateCreated = x.Date_modified

                  }).ToList();
        }

        public int ManageSalaryRemarks(EmployeeSalaryRemark _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.USP_H_MANAGE_SALARY_INFO_REMARKS(
                _model.Mode,
                _model.Id,
                _model.EmpId,
                _model.Remarks,
                _model.RemarksTypeId,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //==============================SALARY REMARKS================================

        //==============================PAYROLL ADJUSTMENT================================
        public List<EmployeeAdjustment> GetEmployeeAdjustments(int _empid)
        {
            return (from d in _conn.AdjustmentFiles
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                  .Select(x => new EmployeeAdjustment()
                  {
                      Id = x.AdjusmentFile_ID,
                      EmpId = x.Emp_ID,
                      AdjustmentId = x.Adjustment_ID,
                      AdjustmentDescription = x.Adjustment.Adjustment_Desc,
                      TranDate = x.Trandate,
                      Amount = x.Amount,
                      IsTaxable = x.Taxable,
                      Billable = x.billable,
                      Status = x.Status == true ? "Processed" : "Unprocessed",
                      Remarks = x.Remarks,
                      UserId = x.UserID,
                      CreatedBy = x.SYS_USER.username,
                  }).ToList();
        }

        public EmployeeAdjustment GetEmployeeAdjustment(int _id)
        {
            return (from d in _conn.AdjustmentFiles
                    where d.AdjusmentFile_ID == _id
                    select d).AsEnumerable()
                  .Select(x => new EmployeeAdjustment()
                  {
                      Id = x.AdjusmentFile_ID,
                      EmpId = x.Emp_ID,
                      AdjustmentId = x.Adjustment_ID,
                      AdjustmentDescription = x.Adjustment.Adjustment_Desc,
                      TranDate = x.Trandate,
                      Amount = x.Amount,
                      IsTaxable = x.Taxable,
                      Billable = x.billable,
                      Status = x.Status == true ? "Processed" : "Unprocessed",
                      Remarks = x.Remarks,
                      UserId = x.UserID,
                      CreatedBy = x.SYS_USER.username,
                  }).SingleOrDefault();
        }

        public int ManageEmployeeAdjustment(EmployeeAdjustment _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.SP_MANAGE_EMPLOYEE_ADJUSTMENT(
                _model.Mode,
                _model.EmpId,
                _model.AdjustmentId,
                _model.TranDate,
                _model.Amount,
                _model.Remarks,
                _model.IsTaxable,
                true,
                _model.Billable,
                _model.Id,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //==============================PAYROLL ADJUSTMENT================================

        //==============================PAYROLL ADJUSTMENT================================
        public List<EmployeeDeduction> GetEmployeeDeductions(int _empid)
        {
            return (from d in _conn.OtherDeductions
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                  .Select(x => new EmployeeDeduction()
                  {
                      Id = x.OtherDed_ID,
                      EmpId = x.Emp_ID,
                      DeductionId = x.Ded_ID,
                      DeductionDescription = x.Deduction.Ded_Desc,
                      TranDate = x.Trandate,
                      Amount = x.Amount,
                      Status = x.Status == true ? "Processed" : "Unprocessed",
                      Remarks = x.Remarks,
                      UserId = x.UserID,
                      CreatedBy = x.SYS_USER.username,
                  }).ToList();
        }

        public EmployeeDeduction GetEmployeeDeduction(int _id)
        {
            return (from d in _conn.OtherDeductions
                    where d.OtherDed_ID == _id
                    select d).AsEnumerable()
                  .Select(x => new EmployeeDeduction()
                  {
                      Id = x.OtherDed_ID,
                      EmpId = x.Emp_ID,
                      DeductionId = x.Ded_ID,
                      DeductionDescription = x.Deduction.Ded_Desc,
                      TranDate = x.Trandate,
                      Amount = x.Amount,
                      Status = x.Status == true ? "Processed" : "Unprocessed",
                      Remarks = x.Remarks,
                      UserId = x.UserID,
                      CreatedBy = x.SYS_USER.username,
                  }).SingleOrDefault();
        }

        public int ManageEmployeeDeduction(EmployeeDeduction _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));

            _conn.SP_MANAGE_EMPLOYEE_DEDUCTION(
                _model.EmpId,
                _model.DeductionId,
                _model.TranDate,
                _model.DayAbsent,
                _model.Amount,
                _model.Remarks,
                true, 
                _model.Id,
                _model.Balance,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //==============================PAYROLL ADJUSTMENT================================

        //==============================PAYROLL SALARY INFO================================
        public EmployeeSalary GetEmployeeSalary(int _empid)
        {
            return (from d in _conn.Employees
                    where d.Emp_ID == _empid
                    select d).AsEnumerable()
                 .Select(x => new EmployeeSalary()
                 {
                     EmpId = x.Emp_ID,
                     BasicRate = x.MonthlySalary,
                     Allowance = x.Allowance,
                     Cola = x.Cola,
                     RiceAllowance = x.RiceAllowance,
                     Deminimis = x.MealAllowance,


                     BankId = x.bank_id,
                     BankName = _bankrepository.Get(int.Parse(x.bank_id.ToString())).BankDescription,
                     AccountNo = x.Account_no,
                     CardNo = x.card_number,
                     CardValidity = x.card_no_validity,

                     IsManualPagibig = x.ismanual_pagibigcont,
                     PagIbigContribution = x.PagIbigCont,

                     IsManualSSS = x.ismanual_ssscont,
                     SSSContribution = x.sss_manualcont,

                     IsManualPhilhealth = x.ismanual_philhealthcont,
                     PhilhealthContribution = x.philhealth_manualcont,

                     IsManaulTax = x.ismanual_tax,
                     ManualTaxDeduction = x.ManualTaxDed,

                     IncludeInPayroll = x.IsIncludePayroll,
                     MinimumWageEarner = x.is_minimum,

                     IsConfidential = x.is_confidential,
                     IsWithVat = x.with_VAT,
                     VatPercentage = x.tax_percentage,


                     ContractTypeId = x.contracttype_id,
                     ContractType = x.REC_CONTRACT_TYPE.contract_type,

                     SourceTypeId = x.employee_processingtype,
                     SourceType = x.REC_SOURCE_TYPE.source_type,

                     PayrollType = x.pay_type,

                     RestDayId = x.restday_id,
                     RestDayDesription = x.RestDay_SetUp.Description,

                     CreatedByUser = x.SYS_USER.username,
                     DateCreated = x.date_encoded
                 }).SingleOrDefault();
        }

        public bool ManageEmployeeSalary(EmployeeSalary _model)
        {            
            try
            {
                _conn.SP_PAYROLL_UPDATE_EMPLOYEE_SALARY(
                    _model.EmpId,
                    _model.BasicRate,
                    _model.Deminimis,
                    _model.RiceAllowance,
                    "",

                    0,
                    _model.Allowance,
                    _model.AccountNo,
                    _model.PagIbigContribution,
                    _model.ManualTaxDeduction,

                    _model.Cola,
                    "",
                    _model.CreatedByUserId,
                    _model.IncludeInPayroll,
                    0, 
                    
                    0, 0, 0,
                    _model.ContractTypeId,
                    _model.SourceTypeId,

                    _model.PayrollType,
                    _model.Remarks,
                    _model.IsManualSSS,
                    _model.IsManualPagibig,
                    _model.IsManualPhilhealth,

                    _model.IsManualSSS,
                    _model.PhilhealthContribution,
                    _model.SSSContribution,
                    _model.MinimumWageEarner,                                                         
                    _model.RestDayId,

                    _model.IsConfidential,
                    _model.CardNo,
                    _model.IsWithVat,
                    _model.VatPercentage,
                    _model.BankId,

                    _model.CardValidity
                );


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            
        }
        //==============================PAYROLL SALARY INFO================================

    }
}