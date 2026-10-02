using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientAdjustmentRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientAdjustmentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<ClientAdjustment_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.Adjustments
                    where x.client_id == _clientid && x.status == true
                    select x
            ).AsEnumerable()
            .Select(d => new ClientAdjustment_vw_model()
            {
                Id = int.Parse(d.Adjustment_ID.ToString()),
                Description = d.Adjustment_Desc,
                Classification = d.ADJUSTMENT_CLASSIFICATION.classification,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.CreationDate.ToShortDateString()
            }).ToList();
        }

        public List<ClientAdjustment_vw_model> GetClassificationList()
        {
            return (from x in _conn.ADJUSTMENT_CLASSIFICATION
                    select x)
                    .AsEnumerable()
                    .Select(d => new ClientAdjustment_vw_model()
                    {
                        Id = d.id,
                        Classification = d.classification,
                        Status = (d.status == true || d.status.ToString() == "1") ? "Active" : "Inactive"
                    })
                    .ToList();
        }

        public ClientAdjustment_model Get(int _id)
        {
            return (from x in _conn.Adjustments
                    where x.Adjustment_ID == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientAdjustment_model()
            {
                Id = int.Parse(d.Adjustment_ID.ToString()),
                ClientId = d.client_id,
                Description = d.Adjustment_Desc,
                Classification = d.ADJUSTMENT_CLASSIFICATION.classification,
                ClassificationId = d.adjustment_classification_id,
                UserId = d.UserID,
                DateCreated = d.CreationDate
            }).SingleOrDefault();
        }

        public int Manage(ClientAdjustment_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_MASTER_ADJUSTMENT(
                _model.Mode,
                _model.Description,
                _model.Id,
                _model.ClientId,                
                _model.UserId,
                false,
                _model.ClassificationId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}