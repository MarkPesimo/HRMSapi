using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientBankRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientBankRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<SysBank_vw_model> GetList()
        {
            return (from x in _conn.SYS_BANK
                    select x
            ).AsEnumerable()
            .Select(d => new SysBank_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                BankCode = d.Bank_code,
                BankName = d.Bank_Name,
                AcctCode = d.acct_code,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public List<ClientBank_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.REC_CLIENT_BANK
                    where x.client_id == _clientid
                    select x
            ).AsEnumerable()
            .Select(d => new ClientBank_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                AccountNo = d.account_no,
                BankName = d.SYS_BANK.Bank_Name,
                ContactPerson = d.Contact_Person,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_added.ToShortDateString()
            }).ToList();
        }

        public ClientBank_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT_BANK
                    where x.id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientBank_model()
            {
                Id = int.Parse(d.id.ToString()),
                ClientId = d.client_id,
                BankId = d.Bank_id,
                BankName = d.SYS_BANK.Bank_Name,
                AccountNo = d.account_no,
                Address = d.bank_address,
                ContactPerson = d.Contact_Person,
                Status = d.status == true ? "Active" : "Inactive",
                UserId = d.user_id,
                DateCreated = d.date_added
            }).SingleOrDefault();
        }

        public int Manage(ClientBank_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CLIENT_BANK(
                _model.Id,
                _model.ClientId,
                _model.AccountNo,
                _model.BankId,
                _model.Address,
                _model.ContactPerson,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}