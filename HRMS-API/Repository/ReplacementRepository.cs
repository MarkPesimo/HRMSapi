using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Floating.Replacement_model;

namespace HRMS_API.Repository
{
    public class ReplacementRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }

        public ReplacementRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        public EmployeeReplacement_vw_model GetReplacementRecord(int _floatingid)
        {
            return (from d in _conn.Employee_Replacement_HD
                    where d.floating_id == _floatingid
                    select d).AsEnumerable()
              .Select(x => new EmployeeReplacement_vw_model()
              {
                  Id = int.Parse(x.id.ToString()),
                  ReplacementDate = x.replacement_date,
                  Remarks = x.remarks,
              }).SingleOrDefault();
        }

        public int ManageReplacement(EmployeeReplacement_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_REPLACEMENT_HD(
                    _model.Mode,
                    _model.Id,
                    _model.EmpId,
                    _model.ReplacementDate,
                    _model.Remarks,
                    _model.FloatingId,
                    _model.SeparationId,
                    _model.Userid, 
                    _return_value );

                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }
    }
}