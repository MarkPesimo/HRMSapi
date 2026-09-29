using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.ClientSetting_model;
using static HRModel.ViewModel.Client.ClientSetting_model.PayrollSetup;

namespace HRMS_API.Repository.Client
{
    public class ClientSetupRepository
    {
        public static apwdbEntities _conn { get; set; }

        public ClientSetupRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
        }

        public string GetCutoff(int _cutoff)
        {
            string _return = "";

            if (_cutoff == 1) { _return= "1st Cut Off"; }
            else if (_cutoff == 2) { _return = "2nd Cut Off"; }
            else if (_cutoff == 3) { _return = "Both"; }

            return _return;
        }

        public int GetReverseCutoff(string _cutoff)
        {
            int  _return = 0;

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
                                              SSSSDeduction = GetCutoff( x.sss_sched),
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
                GetReverseGovernmentMandatedComputationBasis( _model.ClientPayrollSetup.SSSComputationBasis),
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

                                             IncludeThirteenMonth  = x.is_include_thirteen,
                                             ThirteenMonthBasis = x.thirteen_month_pay_basis,

                                             IncludeSepartionPay = x.is_include_separation_pay,
                                             SeparationCategory = x.separation_pay_basis,
                                             SeparationBasis = x.sep_pay_basis,

                                             IncludeHMO= x.is_include_hmo,
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

            if (_obj != null)
            {
                REC_CLIENT_SETUP_EXT _ext = (from d in _conn.REC_CLIENT_SETUP_EXT where d.client_id == _clientid select d).SingleOrDefault();
                if (_ext != null)
                {
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

            }

            return _obj;
        }

        public int ManageInterimSetup(InterimSetup _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MASTER_CLIENT_SETUP_EXT(
                _model.Id,
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

        public PayrollService GetPayrollServiceSetup(int _clientid)
        {
            return   (from d in _conn.REC_CLIENT_SETUP
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
                _model.RegularProcessingFee ,
                _model.SpecialProcessingFee, 
                _model.CustomizationFee ,
                _model.AdditionalFee, 
                _model.ReimbursementOfOutPocketExpenseFee,
                _model.Userid,
                _model.Mode,
                _return_value 
            );

            return Convert.ToInt32(_model.ClientId);
        }

    }
}