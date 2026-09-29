using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;
using static HRModel.ViewModel.Client.ClientHistory_model;

namespace HRMS_API.Repository.Client
{
    public class ClientHistoryRepository
    {
        public static apwdbEntities _conn { get; set; }

        public ClientHistoryRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
        }

        public List<ClientJobOrder_vw_model> GetJobOrders(int _clientid)
        {
            return (from x in _conn.REC_JOB_ORDER
                    where x.client_id == _clientid
                    select x
               ).AsEnumerable()
               .Select(d => new ClientJobOrder_vw_model()
               {
                   JONo = d.id,
                   JODate = d.jo_date.ToShortDateString(),
                   Position = d.position,
                   JOStatus = d.jo_status.ToString() == "1" ? "Open" : "Closed",
                   FeeAmount = d.estimated_salary.ToString(),
                   Quantity = d.quantity,                   
                   CreatedBy = d.SYS_USER.username
               }).ToList();
        }

        public List<ClientPayrollRegister_vw_model> GetPayrollRegisters(int _clientid, int _month, int _year)
        {
            return (from x in _conn.USP_M_GET_PAYROLL_MONITORING(true, _clientid,
                false, 0, 
                _month, _year,
                "", 
                false, DateTime.Now, DateTime.Now,
                false, 0,
                false, 0)                                    
                    select x
              ).AsEnumerable()
              .Select(d => new ClientPayrollRegister_vw_model()
              {
                  PayID = d.payroll_id,
                  PayrollDate = d.pay_date.Value.ToShortDateString(),
                  Status = d.status == 1 ? "Posted" : "Unposted",
                  PayrollType = d.pay_type,
                  PayrollTitle = d.title,
                  HeadCount = d.head_count,
                  NetAmount = d.net.ToString(),
                  CreatedBy = d.username
              }).ToList();
        }

        public List<ClientServiceInvoice_vw_model> GetInvoices(int _clientid, int _month, int _year)
        {
            DateTime _from;
            DateTime _to;

            _from = DateTime.Parse(_month.ToString() + "/1/" + _year.ToString());
            _to = DateTime.Parse(_month.ToString() + "/" +
                DateTime.DaysInMonth(_year, _month).ToString() + "/" +
                _year.ToString());

            return (from x in _conn.USP_B_GET_SALES_INVOICE_MONITORING(true, _clientid,
               false, 0,
               true, _from, _to,
               false, "",
               "", 
               false, 0,
               false, "",
               false, "")
                    select x
             ).AsEnumerable()
             .Select(d => new ClientServiceInvoice_vw_model()
             {
                 SIID = d.id,
                 InvoiceNo = d.soa_no,
                 InvoiceDate = d.soa_date.Value.ToShortDateString(),
                 Particulars = d.particulars,
                 InvoiceType = d.InvoiceType,                 
                 Status = d.file_status.ToString() == "1" ? "Posted" : "Unposted",                 
                 CreatedBy = d.username
             }).ToList();
        }

        public List<ClientCreditMemo_vw_model> GetCreditMemos(int _clientid, int _month, int _year)
        {
            DateTime _from;
            DateTime _to;

            _from = DateTime.Parse(_month.ToString() + "/1/" + _year.ToString());
            _to = DateTime.Parse(_month.ToString() + "/" +
                DateTime.DaysInMonth(_year, _month).ToString() + "/" +
                _year.ToString());

            return (from x in _conn.USP_B_GET_CREDIT_MEMO_MONITORING(true, _clientid,
               false, "",
               true, _from, _to,
               false, "",
               "")
                    select x
             ).AsEnumerable()
             .Select(d => new ClientCreditMemo_vw_model()
             {
                 CMID = d.id,
                 CMNo = d.cm_no,
                 CMDate = d.cm_date.Value.ToShortDateString(),
                 Particulars = d.particulars,
                 CMType = d.cm_type,
                 Status = d.status.ToString() == "1" ? "Posted" : "Unposted",
                 InvoiceFee =d.inv_sub_total.ToString(),
                 VatAmount = d.vat_amount.ToString(),
                 CreatedBy = d.username
             }).ToList();
        }

        public List<ClientOfficialReceipt_vw_model> GetOfficialReceipts(int _clientid, int _month, int _year)
        {
            DateTime _from;
            DateTime _to;

            _from = DateTime.Parse(_month.ToString() + "/1/" + _year.ToString());
            _to = DateTime.Parse(_month.ToString() + "/" +
                DateTime.DaysInMonth(_year, _month).ToString() + "/" +
                _year.ToString());

            return (from x in _conn.USP_B_GET_OFFICIAL_RECEIPT_MONITORING(true, _clientid,               
               true, _from, _to,
               false, "",
               "",
               false, 0)
                    select x
             ).AsEnumerable()
             .Select(d => new ClientOfficialReceipt_vw_model()
             {
                 ORID = d.id,
                 ORNo = d.or_no,
                 ORDate = d.or_date.Value.ToShortDateString(),
                 Particulars = d.particulars,
                 PaymentType = d.payment_type,
                 Status = d.status,
                 ORAmount = d.or_amount.ToString(),
                 AppliedAmount = d.applied_amount.ToString(),
                 OverPayment = (d.or_amount - d.applied_amount).ToString(),
                 CreatedBy = d.username
             }).ToList();
        }
    }
}