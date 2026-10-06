using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientContactRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientContactRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<ClientContact_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.REC_CLIENT_CONTACTS
                    where x.client_id == _clientid && x.file_status == true
                    select x
            ).AsEnumerable()
            .Select(d => new ClientContact_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                ContactPerson = d.contact_person,
                ContactNo = d.contact_no,
                EmailAddress = d.email_address,
                PositionTitle = d.Position,
                Status = d.file_status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public ClientContact_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT_CONTACTS
                    where x.id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientContact_model()
            {
                Id = int.Parse(d.id.ToString()),
                ClientId = d.client_id,
                ContactPerson = d.contact_person,
                ContactNo = d.contact_no,
                EmailAddress = d.email_address,
                PositionTitle = d.Position,
                UserId = d.user_id,
                DateCreated = d.date_created
            }).SingleOrDefault();
        }

        public int Manage(ClientContact_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_B_MANAGE_REC_CLIENT_CONTACTS(
                _model.Id,
                _model.ClientId,
                _model.ContactPerson,
                _model.ContactNo,
                _model.EmailAddress,
                true,
                _model.PositionTitle,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}