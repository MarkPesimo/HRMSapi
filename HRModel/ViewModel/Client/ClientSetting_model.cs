using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Client
{
    public class ClientSetting_model
    {
        public class PayrollSetup
        {
            public int ClientId { get; set; }
            public PayrollSetup_model ClientPayrollSetup { get; set; }
            public CutoffSetup_model ClientCutoffSetup { get; set; }
            public int Userid { get; set; }
            public string CreatedBy { get; set; }
            public int Mode { get; set; }

            public class PayrollSetup_model
            {
                public int Id { get; set; }
                public string SSSSDeduction { get; set; }
                public string PhilhealthDeduction { get; set; }
                public string PagibigDeduction { get; set; }

                public string Allowance { get; set; }
                public string AllowanceBasic { get; set; }
                public string Meal { get; set; }
                public string Rice { get; set; }

                public int FirstCutoffPayrollDate { get; set; }
                public int SecondCutoffPayrollDate { get; set; }

                public string SSSComputationBasis { get; set; }
                public string PhilhealthComputationBasis { get; set; }

                public string SSSLoanDeductionSetup { get; set; }
                public string pagIbigLoanDeductionSetup { get; set; }
                public string OtherLoanDeductionSetup { get; set; }
            }

            public class CutoffSetup_model
            {
                public int Id { get; set; }
                public string FirstCutoffFromMonth { get; set; }
                public int FirstCutoffFromDay { get; set; }
                public string FirstCutoffToMonth { get; set; }
                public int FirstCutoffToDay { get; set; }

                public string SecondCutoffFromMonth { get; set; }
                public int SecondCutoffFromDay { get; set; }
                public string SecondCutoffToMonth { get; set; }
                public int SecondCutoffToDay { get; set; }
            }
        }

        public class InterimSetup
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public decimal SourceFee { get; set; }
            public decimal EndorseFee { get; set; }
            public decimal SeasonalRate { get; set; }
            public bool IncludeThirteenMonth { get; set; }
            public string ThirteenMonthBasis { get; set; }
            public bool IncludeSepartionPay { get; set; }
            public string SeparationCategory { get; set; }
            public string SeparationBasis { get; set; }
            public bool IncludeHMO { get; set; }
            public decimal HMORate { get; set; }
            public bool IncludeSILP { get; set; }
            public decimal SILPDays { get; set; }
            public string SILPBasic { get; set; }
            public bool IncludeWard { get; set; }
            public decimal WardRate { get; set; }
            public bool IncludeAllowance { get; set; }
            public bool IncludeInsurance { get; set; }
            public decimal InsuranceRate { get; set; }
            public bool IncludeBillableAdjustment { get; set; }
            public string BillableAdjustmentBasis { get; set; }

            public bool AllowProcessingFee { get; set; }
            public bool AllowBillingRate { get; set; }
            public bool AllowBillingCard { get; set; }

            public bool ImmediateTerm { get; set; }
            public bool FifteenDaysTerm { get; set; }
            public bool TwentyDaysTerm { get; set; }
            public bool ThirtyDaysTerm { get; set; }
            public bool FortyDaysTerm { get; set; }
            public bool FortyFiveDaysTerm { get; set; }
            public bool SixtyDaysTerm { get; set; }
            public bool NinetyDaysTerm { get; set; }

            public bool ComputationTypeAll { get; set; }
            public bool ComputationTypeSalaryOT { get; set; }
            public bool ComputationTypeSalaryOnly { get; set; }
            public bool ComputationTypeOTOnly { get; set; }
            public bool ComputationTypeAdjustmentOnly { get; set; }

            public int Mode { get; set; }
            public int Userid { get; set; }
        }

        public class PayrollService
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int MaxExployeeProcessCount { get; set; }
            public decimal RegularProcessingFee { get; set; }
            public decimal SpecialProcessingFee { get; set; }
            public decimal CustomizationFee { get; set; }
            public decimal AdditionalFee { get; set; }
            public decimal ReimbursementOfOutPocketExpenseFee { get; set; }
            public int Userid { get; set; }
            public int Mode { get; set; }
        }

        public class OvertimeRate
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public decimal Reg { get; set; }
            public decimal RegND { get; set; }
            public decimal RegN8 { get; set; }

            public decimal Rd { get; set; }
            public decimal RdND { get; set; }
            public decimal RdN8 { get; set; }

            public decimal SHMonthly { get; set; }
            public decimal SHDaily { get; set; }
            public decimal SHND { get; set; }
            public decimal SHN8 { get; set; }

            public decimal LHMonthly { get; set; }
            public decimal LHDaily { get; set; }
            public decimal LHND { get; set; }
            public decimal LHN8 { get; set; }

            public decimal RDSH { get; set; }
            public decimal RDSHND { get; set; }
            public decimal RDSHN8 { get; set; }

            public decimal RDLH { get; set; }
            public decimal RDLHND { get; set; }
            public decimal RDLHN8 { get; set; }

            public int UserId { get; set; }
            public int Mode { get; set; }
        }


        public class AccountMapping
        {
            public class Account_model
            {
                public int Id { get; set; }
                public int ClientId { get; set; }
                public decimal SortNo { get; set; }
                public string AccountNo { get; set; }
                public string AccountDescription { get; set; }
                public string AccountType { get; set; }
                public string MappingType { get; set; }
                public string EntryType { get; set; }
                public bool ByDepartment { get; set; }
                public bool IsMinimun { get; set; }

                public int UserId { get; set; }
                public int Mode { get; set; }
                public string CreatedBy { get; set; }
                public DateTime DateCreated { get; set; }
            }


            public class AccountDetail_model
            {
                public int Id { get; set; }
                public int AccountId { get; set; }
                public int LinkId { get; set; }
                public string Description { get; set; }

                public int UserId { get; set; }
                public int Mode { get; set; }
                public string CreatedBy { get; set; }
                public DateTime DateCreated { get; set; }
            }
        }
    }
}
