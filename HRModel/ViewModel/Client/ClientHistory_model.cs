using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Client
{
    public class ClientHistory_model
    {
        //========================CLIENT JOB ORDER RECORDS========================
        public class ClientJobOrder_vw_model
        {
            public int JONo { get; set; }
            public string JODate { get; set; }
            public string Position { get; set; }
            public string JOStatus { get; set; }
            public string FeeAmount { get; set; }
            public int Quantity { get; set; }
            public string CreatedBy { get; set; }
        }

        //========================CLIENT PAYROLL REGISTER========================
        public class ClientPayrollRegister_vw_model
        {
            public int? PayID { get; set; }
            public string PayrollDate { get; set; }
            public string Status { get; set; }
            public string PayrollType { get; set; }
            public string PayrollTitle { get; set; }
            public int? HeadCount { get; set; }
            public string NetAmount { get; set; }
            public string CreatedBy { get; set; }
        }

        //========================CLIENT INVOICE========================
        public class ClientServiceInvoice_vw_model
        {
            public int? SIID { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public string Particulars { get; set; }
            public string InvoiceType { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
        }

        //========================CLIENT INVOICE========================
        public class ClientCreditMemo_vw_model
        {
            public int? CMID { get; set; }
            public string CMNo { get; set; }
            public string CMDate { get; set; }
            public string Particulars { get; set; }
            public string CMType { get; set; }
            public string Status { get; set; }
            public string InvoiceFee { get; set; }
            public string VatAmount { get; set; }
            public string CreatedBy { get; set; }
        }

        //========================CLIENT OFFICIAL RECIEPT========================
        public class ClientOfficialReceipt_vw_model
        {
            public int? ORID { get; set; }
            public string ORNo { get; set; }
            public string ORDate { get; set; }
            public string Particulars { get; set; }
            public string PaymentType { get; set; }
            public string Status { get; set; }
            public string ORAmount { get; set; }
            public string AppliedAmount { get; set; }
            public string OverPayment { get; set; }
            public string CreatedBy { get; set; }
        }

    }
}
