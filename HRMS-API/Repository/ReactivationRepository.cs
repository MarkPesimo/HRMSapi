using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Contract.EmployeeTransaction.ReactivationModel;

namespace HRMS_API.Repository
{
    public class ReactivationRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }

        public ReactivationRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        public List<EmployeeReactivationMonitoring_model> GetMonitoring(bool _bystatus, bool _status, string _keyword)
        {
            string sanitizedKeyword = string.IsNullOrWhiteSpace(_keyword) || _keyword == "NULL" ? null : _keyword;

            return (from d in _conn.USP_H_EMPLOYEE_SEPARATION_REOPEN_MONITORING(_bystatus, _status, _keyword) select d).AsEnumerable()
                .Select(x => new EmployeeReactivationMonitoring_model()
                {
                    Id = int.Parse( x.id.ToString()),
                    DateCreated = DateTime.Parse(x.date_created.ToString()),
                    ClientName = x.client_name,
                    EmpId = int.Parse(x.emp_id.ToString()),
                    EmployeeGUID = x.employee_guid,
                    EmployeeName = x.employee_name,
                    RequestedBy = x.requested_by,
                    Reason = x.reason,
                    Status = x.status,
                    CreatedBy = x.created_by,
                    DateClosed = x.date_closed == null ? "" : x.date_closed.Value.ToShortDateString(),
                    TranId = int.Parse(x.tran_id.ToString())
                }).ToList();
        }

        public void ManageContractReactivation(EmployeeReactivation_model _model)
        {
            try
            {
                _conn.USP_H_MANAGE_EMPLOYEE_SEPARATION_REOPEN(_model.Id,
                    _model.TransactionId,
                    _model.SeparationId,
                    _model.RequestedByUserid,
                    _model.Reason,
                    _model.UserId,
                    _model.Mode);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }

    }
}