using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientShiftRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientShiftRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<ShiftDropdown_model> GetShiftDropdownList()
        {
            return (from x in _conn.Shifts
                    where x.status == true
                    select x
            ).AsEnumerable()
            .Select(d => new ShiftDropdown_model()
            {
                Id = d.Shift_ID,
                Description = d.Description
            }).OrderBy(x => x.Description).ToList();
        }

        public List<ClientShift_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.REC_CLIENT_SHIFT
                    where x.client_id == _clientid && x.status == true
                    select x
            ).AsEnumerable()
            .Select(d => new ClientShift_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                Description = d.Shift.Description,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public ClientShift_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT_SHIFT
                    where x.id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientShift_model()
            {
                Id = int.Parse(d.id.ToString()),
                ClientId = d.client_id,
                ShiftId = d.shift_id,
                Description = d.Shift.Description,
                UserId = d.user_id,
                DateCreated = d.date_created
            }).SingleOrDefault();
        }

        public int Manage(ClientShift_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_T_MANAGE_CLIENT_SHIFT(
                _model.Id,
                _model.ClientId,
                _model.ShiftId,               
                _model.UserId,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}