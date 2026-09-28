using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientDepartmentRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientDepartmentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<ClientDepartment_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.REC_CLIENT_DEPARTMENT
                    where x.client_id == _clientid
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDepartment_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                DepartmentCode = d.dept_code,
                DepartmentName = d.Department.Dept_Name,
                Status = d.status == true ?  "Active" : "Inactive",
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public ClientDepartment_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT_DEPARTMENT
                    where x.id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDepartment_model()
            {
                Id = int.Parse(d.id.ToString()),
                ClientId = d.client_id,
                DepartmentCode = d.dept_code,
                DepartmentName = d.Department.Dept_Name,
                DepartmentId = d.department_id,
                UserId = d.user_id,
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created
            }).SingleOrDefault();
        }

        public int Manage(ClientDepartment_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_I_MANAGE_CLIENT_DEPARTMENT(
                _model.Mode,
                _model.Id,
                _model.ClientId,
                _model.DepartmentCode,
                _model.DepartmentId,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}