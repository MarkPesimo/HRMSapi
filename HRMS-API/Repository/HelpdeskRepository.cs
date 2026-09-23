using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Helpdesk.Helpdesk_model;

namespace HRMS_API.Repository
{
    public class HelpdeskRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public HelpdeskRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<HelpdeskMonitoring_model> GetHelpdeskMonitoring(HelpdeskMonitoringFilter_model _filter)
        {
            return (from d in _conn.USP_H_GET_HELPDESK_MONITORING(
                _filter.UserId,
                _filter.UserType,
                _filter.ByConcernType,
                _filter.ConcernTypeID,
                _filter.ByStatus,
                _filter.ConcernStatus,
                _filter.ByDate,
                _filter.DateFrom,
                _filter.DateTo,
                _filter.Keyword,
                _filter.ByClient,
                _filter.ClientId,
                _filter.CompanyId) 
                    select d).AsEnumerable()
               .Select(x => new HelpdeskMonitoring_model()
               {
                   Id = int.Parse(x.id.ToString()),
                   ConcernDate = DateTime.Parse( x.concern_date.ToString()),
                   ClientName = x.client,
                   ConcernType = x.concern_type,
                   ConcernDescriprion = x.concern_description,
                   FileStatus = x.file_status,
                   ConcernStatus = x.concern_status,
                   CreatedBy = x.created_by,
                   DateCompleted = x.date_completed ,
                   Age = int.Parse(x.age.ToString()),
                   DatePosted = x.date_posted
               }).ToList();
        }

        public HelpdeskConcern_model GetHelpdeskConcern(int _id)
        {
           return  (from d in _conn.HELPDESK_CONCERN
                                                 where d.id == _id
                                                 select d).AsEnumerable()
              .Select(x => new HelpdeskConcern_model()
              {
                  Id = int.Parse(x.id.ToString()),
                  PortalUserId = int.Parse(x.user_id.ToString()),
                  UserType = x.user_type,
                  ConcernTypeId = x.concern_type_id,
                  ConcernDescription = x.concern_description,
                  DateCreated = x.date_created,
                  UserId = 0,
                  Mode = 0
              }).SingleOrDefault();
        }

        public List<ConcernThread> GetConcernThreads(int _concernid)
        {
            return (from d in _conn.USP_H_GET_COMMENTS(_concernid )
                    select d).AsEnumerable()
               .Select(x => new ConcernThread()
               {
                   Sort = decimal.Parse(x.sort.ToString()),
                   Id = int.Parse(x.id.ToString()),
                   Username = x.username,
                   DateCreated = DateTime.Parse( x.date_created.ToString()),
                   Remarks = x.remarks,
                   Usertype = x.user_type,
                   TimeCreated = x.time_created                    
               }).ToList();
        }


        public int ManageHelpdeskConcern(HelpdeskConcern_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_CONCERN(
                    _model.Id,
                    _model.PortalUserId,
                    _model.UserType,
                    _model.ConcernTypeId,
                    _model.ConcernDescription,
                    DateTime.Now,
                    _model.DateCreated,
                    _model.Mode,
                    _model.UserId,
                    _return_value);

                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }

        public int ManageHelpdeskConcernThread(HelpdeskConcernThread_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_CONCERN_THREAD(
                    _model.Id,
                    _model.ConcernId,
                    _model.UserType,
                    _model.Remarks,
                    _model.FileLocation,
                    _model.FileName,
                    _model.ContentType,
                    _model.DateUploaded,
                    _model.Mode,
                    _model.UserId, 
                    _return_value);

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