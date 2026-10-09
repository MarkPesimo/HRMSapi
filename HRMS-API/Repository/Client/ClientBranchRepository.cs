using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientBranchRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientBranchRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<SysRegion_vw_model> GetRegionDropdown()
        {
            return (from x in _conn.SYS_REGION
                    select x
            ).AsEnumerable()
            .Select(d => new SysRegion_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                RegionCode = d.region_code,
                RegionName = d.region_name,
                Status = d.status == true ? "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public List<ClientBranch_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.Branches
                    where x.client_id == _clientid
                    select x
            ).AsEnumerable()
            .Select(d => new ClientBranch_vw_model()
            {
                Id = int.Parse(d.Branch_ID.ToString()),
                BranchName = d.Branch_Desc,
                NoOfDays= d.no_of_days,
                RegionNo = d.region_name,
                Status = d.status == true ?  "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public ClientBranch_model Get(int _id)
        {
            return (from x in _conn.Branches
                    where x.Branch_ID == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientBranch_model()
            {
                Id = int.Parse(d.Branch_ID.ToString()),
                ClientId = d.client_id,
                BranchName = d.Branch_Desc,
                NoOfDays = d.no_of_days,
                RegionNo = d.region_name,
                UserId = d.UserID,
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created
            }).SingleOrDefault();
        }

        public int Manage(ClientBranch_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_MASTER_BRANCH(
                _model.Mode,
                _model.BranchName,
                _model.ClientId,
                _model.NoOfDays,
                _model.RegionNo,
                _model.Id,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}