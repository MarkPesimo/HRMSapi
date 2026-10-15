using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientDeductionRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientDeductionRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<ClientDeduction_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.Deductions
                    where x.client_id == _clientid
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDeduction_vw_model()
            {
                Id = int.Parse(d.Ded_id.ToString()),
                Description = d.Ded_Desc,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_Created.ToShortDateString()
            }).ToList();
        }

        public ClientDeduction_model Get(int _id)
        {
            return (from x in _conn.Deductions
                    where x.Ded_id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDeduction_model()
            {
                Id = int.Parse(d.Ded_id.ToString()),
                ClientId = d.client_id,
                Description = d.Ded_Desc,                               
                UserId = d.UserID,
                DateCreated = d.date_Created
            }).SingleOrDefault();
        }

        public int Manage(ClientDeduction_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_MASTER_DEDUCTIONS(
                _model.Mode,
                _model.Description,
                _model.Id,
                _model.ClientId,
                _model.UserId,             
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}