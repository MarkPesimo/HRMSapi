using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.ClientSetting_model;
using static HRModel.ViewModel.Client.ClientSetting_model.AccountMapping;
using static HRModel.ViewModel.Client.ClientSetting_model.PayrollSetup;

namespace HRMS_API.Repository.Client
{
    public class ClientSetupRepository
    {
        public static apwdbEntities _conn { get; set; }
        public UserRepository _userrepository { get; set; }


        public ClientSetupRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        //==================================PAYROLL SETUP===========================================
        public string GetCutoff(int _cutoff)
        {
            string _return = "";

            if (_cutoff == 1) { _return = "1st Cut Off"; }
            else if (_cutoff == 2) { _return = "2nd Cut Off"; }
            else if (_cutoff == 3) { _return = "Both"; }

            return _return;
        }

        public int GetReverseCutoff(string _cutoff)
        {
            int _return = 0;

            if (_cutoff == "1st Cut Off") { _return = 1; }
            else if (_cutoff == "2nd Cut Off") { _return = 2; }
            else if (_cutoff == "Both") { _return = 3; }

            return _return;
        }

        public string GetGovernmentMandatedComputationBasis(int _basis)
        {
            string _return = "";

            if (_basis == 1) { _return = "Monthly Salary"; }
            else if (_basis == 2) { _return = "Total Basic Pay"; }
            else if (_basis == 3) { _return = "Total Gross Pay"; }

            return _return;
        }

        public int GetReverseGovernmentMandatedComputationBasis(string _basis)
        {
            int _return = 0;

            if (_basis == "Monthly Salary") { _return = 1; }
            else if (_basis == "Total Basic Pay") { _return = 2; }
            else if (_basis == "Total Gross Pay") { _return = 3; }

            return _return;
        }

        public List<OvertimeBasis_vw_model> GetOvertimeBasisList()
        {
            return (from x in _conn.OvertimeBasis
                    select x
            ).AsEnumerable()
            .Select(d => new OvertimeBasis_vw_model()
            {
                Id = d.Id,
                OvertimeBasisDesc = d.OvertimeBasis_Desc,
                MinTime = d.Min_Time ?? 0,
                SucceedingTime = d.Succeeding_Time ?? 0
            }).ToList();
        }

        public PayrollSetup GetPayrollSetup(int _clientid)
        {
            PayrollSetup _object = new PayrollSetup();
            _object.ClientId = _clientid;
            _object.ClientPayrollSetup = (from d in _conn.REC_CLIENT_SETUP
                                          where d.client_id == _clientid
                                          select d).AsEnumerable()
                                          .Select(x => new PayrollSetup_model()
                                          {
                                              Id = x.id,
                                              SSSSDeduction = GetCutoff(x.sss_sched),
                                              PhilhealthDeduction = GetCutoff(x.philhealth_sched),
                                              PagibigDeduction = GetCutoff(x.pagibig_sched),
                                              Allowance = GetCutoff(x.allowance_sched),
                                              AllowanceBasic = x.allowance_deduct_basis,
                                              Meal = GetCutoff(x.meal_allowance_sched),
                                              Rice = GetCutoff(x.rice_allowance_sched),
                                              FirstCutoffPayrollDate = x.payroll_payment_1st_cutoff,
                                              SecondCutoffPayrollDate = x.payroll_payment_2nd_cutoff,
                                              SSSComputationBasis = GetGovernmentMandatedComputationBasis(x.sss_computation),
                                              PhilhealthComputationBasis = GetGovernmentMandatedComputationBasis(x.philhealth_computation),
                                              SSSLoanDeductionSetup = GetCutoff(x.sss_loan_sched),
                                              pagIbigLoanDeductionSetup = GetCutoff(x.pagibig_loan_sched),
                                              OtherLoanDeductionSetup = GetCutoff(x.other_loan_sched),
                                              
                                          }).SingleOrDefault();

            if (_object != null)
            {
                _object.ClientCutoffSetup = (from d in _conn.REC_CLIENT_SETUP_CUTOFF
                                             where d.client_id == _clientid
                                             select d).AsEnumerable()
                                          .Select(x => new CutoffSetup_model()
                                          {
                                              Id = x.id,
                                              FirstCutoffFromMonth = x.first_cutoff_from_month,
                                              FirstCutoffFromDay = x.first_cutoff_from_day,
                                              FirstCutoffToMonth = x.first_cutoff_to_month,
                                              FirstCutoffToDay = x.first_cutoff_to_day,

                                              SecondCutoffFromMonth = x.second_cutoff_from_month,
                                              SecondCutoffFromDay = x.second_cutoff_from_day,
                                              SecondCutoffToMonth = x.second_cutoff_to_month,
                                              SecondCutoffToDay = x.second_cutoff_to_day
                                          }).SingleOrDefault();

            }

            return _object;
        }

        public int ManagePayrollSetup(PayrollSetup _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_MANAGE_CLIENT_SETUP(
                _model.ClientPayrollSetup.Id,
                _model.ClientId,
                GetReverseCutoff(_model.ClientPayrollSetup.SSSSDeduction),
                GetReverseCutoff(_model.ClientPayrollSetup.PhilhealthDeduction),
                GetReverseCutoff(_model.ClientPayrollSetup.PagibigDeduction),
                GetReverseCutoff(_model.ClientPayrollSetup.Allowance),
                _model.ClientPayrollSetup.AllowanceBasic,
                GetReverseCutoff(_model.ClientPayrollSetup.Meal),
                GetReverseCutoff(_model.ClientPayrollSetup.Rice),
                _model.ClientPayrollSetup.FirstCutoffPayrollDate,
                _model.ClientPayrollSetup.SecondCutoffPayrollDate,
                GetReverseGovernmentMandatedComputationBasis(_model.ClientPayrollSetup.SSSComputationBasis),
                GetReverseGovernmentMandatedComputationBasis(_model.ClientPayrollSetup.PhilhealthComputationBasis),
                GetReverseCutoff(_model.ClientPayrollSetup.SSSLoanDeductionSetup),
                GetReverseCutoff(_model.ClientPayrollSetup.pagIbigLoanDeductionSetup),
                GetReverseCutoff(_model.ClientPayrollSetup.OtherLoanDeductionSetup),
                _model.Userid,
                _model.Mode,
                _return_value
            );

            _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CLIENT_CUTOFF_SETUP(
                _model.ClientId,
                _model.ClientCutoffSetup.FirstCutoffFromMonth,
                _model.ClientCutoffSetup.FirstCutoffFromDay,
                _model.ClientCutoffSetup.FirstCutoffToMonth,
                _model.ClientCutoffSetup.FirstCutoffToDay,

                _model.ClientCutoffSetup.SecondCutoffFromMonth,
                _model.ClientCutoffSetup.SecondCutoffFromDay,
                _model.ClientCutoffSetup.SecondCutoffToMonth,
                _model.ClientCutoffSetup.SecondCutoffToDay,
                _model.Userid
            );

            return Convert.ToInt32(_model.ClientId);
        }
         


        public InterimSetup GetInterimSetup(int _clientid)
        {
            InterimSetup _obj = (from d in _conn.REC_CLIENT_SETUP
                                 where d.client_id == _clientid
                                 select d).AsEnumerable()
                                 .Select(x => new InterimSetup()
                                 {
                                     Id = x.id,
                                     ClientId = x.client_id,
                                     SourceFee = x.source_rate,
                                     EndorseFee = x.endorse_rate,
                                     SeasonalRate = x.seasonal_rate,

                                     IncludeThirteenMonth = x.is_include_thirteen,
                                     ThirteenMonthBasis = x.thirteen_month_pay_basis,

                                     IncludeSepartionPay = x.is_include_separation_pay,
                                     SeparationCategory = x.separation_pay_basis,
                                     SeparationBasis = x.sep_pay_basis,

                                     IncludeHMO = x.is_include_hmo,
                                     HMORate = x.hmo_amount,

                                     IncludeSILP = x.is_include_silp,
                                     SILPDays = x.silp_days,
                                     SILPBasic = x.silp_basis,

                                     IncludeWard = x.is_include_ward,
                                     WardRate = x.ward_amount,

                                     IncludeAllowance = x.is_include_allowance,
                                     IncludeInsurance = x.include_insurance,
                                     InsuranceRate = x.insurance_amount,

                                     IncludeBillableAdjustment = x.include_adjustment,
                                     BillableAdjustmentBasis = x.adjustment_basis
                                 }).SingleOrDefault();

            if (_obj == null)
            {
                _obj = new InterimSetup { ClientId = _clientid };
            }

            REC_CLIENT_SETUP_EXT _ext = (from d in _conn.REC_CLIENT_SETUP_EXT
                                         where d.client_id == _clientid
                                         select d).SingleOrDefault();
            if (_ext != null)
            {
                _obj.ExtId = _ext.id;
                _obj.AllowProcessingFee = _ext.proc_processing_fee;
                _obj.AllowBillingRate = _ext.Proc_billing_rate;
                _obj.AllowBillingCard = _ext.proc_billing_card;

                _obj.ImmediateTerm = _ext.term_immediate;
                _obj.FifteenDaysTerm = _ext.term_fifteen;
                _obj.TwentyDaysTerm = _ext.term_twenty;
                _obj.ThirtyDaysTerm = _ext.term_thirty;
                _obj.FortyDaysTerm = _ext.term_forty;
                _obj.FortyFiveDaysTerm = _ext.term_fourtyfive;
                _obj.SixtyDaysTerm = _ext.term_sixty;
                _obj.NinetyDaysTerm = _ext.term_ninety;

                _obj.ComputationTypeAll = _ext.comp_all;
                _obj.ComputationTypeSalaryOT = _ext.comp_salary_overtime;
                _obj.ComputationTypeSalaryOnly = _ext.comp_salary_only;
                _obj.ComputationTypeOTOnly = _ext.comp_overtime_only;
                _obj.ComputationTypeAdjustmentOnly = _ext.comp_adjustment_only;
            }

            return _obj;
        }

        public int ManageInterimSetup(InterimSetup _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MASTER_CLIENT_SETUP_EXT(
                _model.ExtId,
                _model.ClientId,
                _model.AllowProcessingFee,
                _model.AllowBillingRate,
                _model.ThirtyDaysTerm,
                _model.FortyFiveDaysTerm,
                _model.SixtyDaysTerm,
                _model.NinetyDaysTerm,
                _model.ComputationTypeAll,
                _model.ComputationTypeSalaryOT,
                _model.ComputationTypeSalaryOnly,
                _model.ComputationTypeOTOnly,
                _model.ComputationTypeAdjustmentOnly,
                _model.FifteenDaysTerm,
                _model.ImmediateTerm,
                _model.AllowBillingCard,
                _model.Mode,
                _model.Userid,
                 
                _return_value,
                _model.TwentyDaysTerm,
                _model.FortyDaysTerm
            );

            return Convert.ToInt32(_model.ClientId);
        }
        //==================================INTERIM SETUP===========================================

        //==================================PAYROLL SERVICE SETUP===========================================
        public PayrollService GetPayrollServiceSetup(int _clientid)
        {
            return (from d in _conn.REC_CLIENT_SETUP
                    where d.client_id == _clientid
                    select d).AsEnumerable()
                                          .Select(x => new PayrollService()
                                          {
                                              Id = x.id,
                                              ClientId = x.client_id,
                                              MaxExployeeProcessCount = x.ps_max_process_employee_count,
                                              RegularProcessingFee = x.ps_regular_process_fee,
                                              SpecialProcessingFee = x.ps_special_process_fee,
                                              CustomizationFee = x.ps_one_time_fee,
                                              AdditionalFee = x.ps_additional_fee,
                                              ReimbursementOfOutPocketExpenseFee = x.rope_admin_fee
                                          }).SingleOrDefault();


        }

        public int ManagePayrollServiceSetup(PayrollService _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_MANAGE_CLIENT_SETUP_PAYROLL_SERVICE(
                _model.Id,
                _model.ClientId,
                _model.MaxExployeeProcessCount,
                _model.RegularProcessingFee,
                _model.SpecialProcessingFee,
                _model.CustomizationFee,
                _model.AdditionalFee,
                _model.ReimbursementOfOutPocketExpenseFee,
                _model.Userid,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_model.ClientId);
        }
        //==================================PAYROLL SERVICE SETUP===========================================

        //==================================OVERTIME RATE SETUP===========================================
        public OvertimeRate GetOvertimeRate(int _clientid)
        {
            return (from d in _conn.REC_CLIENT_OT_RATE
                    where d.client_id == _clientid
                    select d).AsEnumerable()
                    .Select(x => new OvertimeRate()
                    {
                        Id = x.id,
                        ClientId = x.client_id,

                        Reg = x.reg_ot_rate,
                        RegND = x.reg_ot_nd_rate,
                        RegN8 = x.reg_ot_nd_rate,

                        Rd = x.rest_day_ot_rate,
                        RdND = x.rest_day_ot_nd_rate,
                        RdN8 = x.rest_day_ot_n8_rate,

                        SHDaily = x.spc_hol_ot_rate_daily,
                        SHMonthly = x.spc_hol_ot_rate,
                        SHND = x.spc_hol_ot_nd_rate,
                        SHN8 = x.spc_hol_ot_n8_rate,

                        LHDaily = x.leg_hol_ot_rate_daily,
                        LHMonthly = x.legal_hol_ot_rate,
                        LHND = x.legal_hol_ot_nd_rate,
                        LHN8 = x.legal_hol_ot_n8_rate,

                        RDSH = x.rest_day_spc_hol_ot_rate,
                        RDSHND = x.rest_day_spc_hol_ot_nd_rate,
                        RDSHN8 = x.rest_day_spc_hol_ot_n8_rate,

                        RDLH = x.rest_day_legal_hol_ot_rate,
                        RDLHND = x.rest_day_legal_hol_nd_ot_rate,
                        RDLHN8 = x.rest_day_legal_hol_n8_ot_rate,
                    }).SingleOrDefault();
        }

        public int ManageOvertimeRate(OvertimeRate _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CLIENT_OT_RATE(
                _model.Id,
                _model.ClientId,
                _model.Reg,  _model.RegN8,  _model.RegND,
                _model.Rd, _model.RdN8, _model.RdND,
                _model.SHMonthly, _model.SHN8, _model.SHND,
                _model.LHMonthly, _model.LHN8, _model.LHND,
                _model.RDSH, _model.RDSHN8, _model.RDSHND,
                _model.RDLH, _model.RDLHN8, _model.RDLHND,
                _model.SHDaily, _model.LHDaily,
                _model.UserId, _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_model.ClientId);
        }
        //==================================OVERTIME RATE SETUP===========================================


        //==================================ACCOUNT MAPPING SETUP===========================================
        public List<Account_model> GetAccounts(int _clientid)
        {
            return (from d in _conn.REC_CLIENT_ACCOUNT_MAPPING
                    where d.client_id == _clientid
                    select d).AsEnumerable()
                   .Select(x => new Account_model()
                   {
                       Id = x.id,
                       ClientId = x.client_id,
                       SortNo = x.sort_no,
                       AccountNo = x.acct_code,
                       AccountType = x.account_type,
                       AccountDescription = x.account_description,
                       MappingType = x.mapping_cnt,
                       EntryType = x.entry_type,
                       ByDepartment = x.by_dept,
                       IsMinimun = x.is_minimum,
                       UserId = x.user_id,
                       DateCreated = x.date_created,
                       CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   }).ToList();
        }

        public Account_model GetAccount(int _id)
        {
            return (from d in _conn.REC_CLIENT_ACCOUNT_MAPPING
                    where d.id == _id
                    select d).AsEnumerable()
                   .Select(x => new Account_model()
                   {
                       Id = x.id,
                       ClientId = x.client_id,
                       SortNo = x.sort_no,
                       AccountNo = x.acct_code,
                       AccountType = x.account_type,
                       AccountDescription = x.account_description,
                       MappingType = x.mapping_cnt,
                       EntryType = x.entry_type,
                       ByDepartment = x.by_dept,
                       IsMinimun = x.is_minimum,
                       UserId = x.user_id,
                       DateCreated = x.date_created,
                       CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   }).SingleOrDefault();
        }

        public int ManageAccount(Account_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CLIENT_ACCOUNT_MAPPING(
                _model.Id,
                _model.ClientId,
                _model.SortNo,
                _model.AccountNo,
                _model.AccountDescription,
                _model.AccountType,
                _model.IsMinimun,
                _model.ByDepartment,
                61,
                _model.MappingType,
                _model.EntryType,
                _model.UserId,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value);
        }

        public AccountDetail_model GetAccountDetail(int _id)
        {
            return (from d in _conn.REC_CLIENT_ACCOUNT_MAP_FIELD
                    where d.id == _id
                    select d).AsEnumerable()
                   .Select(x => new AccountDetail_model()
                   {
                        Id = x.id,
                        AccountId = x.map_id,
                        LinkId = x.entry_id,
                        Description = GetAccountDetailDescription(x.REC_CLIENT_ACCOUNT_MAPPING.entry_type, x.entry_id),
                        UserId = x.user_id,
                        DateCreated = x.date_added,
                        CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   }).SingleOrDefault();
        }

        public List<AccountDetail_model> GetAccountDetails(int _id)
        {
            return (from d in _conn.REC_CLIENT_ACCOUNT_MAP_FIELD
                  where d.map_id == _id
                  select d).AsEnumerable()
                   .Select(x => new AccountDetail_model()
                   {
                       Id = x.id,
                       AccountId = x.map_id,
                       LinkId = x.entry_id,
                       Description = GetAccountDetailDescription(x.REC_CLIENT_ACCOUNT_MAPPING.entry_type, x.entry_id),
                       UserId = x.user_id,
                       DateCreated = x.date_added,
                       CreatedBy = _userrepository.GetUser(x.user_id).UserName,
                   }).ToList();
        }

        public int ManageAccountDetail(AccountDetail_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CLIENT_ACCOUNT_MAP_FIELD(
                _model.Id,
                _model.AccountId,
                _model.LinkId,
                _model.UserId,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value);
        }

        public string GetAccountDetailDescription(string _entrytype, int _id)
        {
            string _return = "";

            if (_entrytype == "ADJUSTMENTS") {
                _return = (from d in _conn.Adjustments where d.Adjustment_ID == _id select d.Adjustment_Desc).SingleOrDefault();
            }
            else if (_entrytype == "DEDUCTIONS") {
                _return = (from d in _conn.Deductions where d.Ded_id == _id select d.Ded_Desc).SingleOrDefault();
            }
            else if (_entrytype == "LOANS") {
                _return = (from d in _conn.LoanTypes where d.LoanType_ID == _id select d.LoanType_Desc).SingleOrDefault();
            }
                       
            return _return;
        }
        //==================================ACCOUNT MAPPING SETUP===========================================

    }
}