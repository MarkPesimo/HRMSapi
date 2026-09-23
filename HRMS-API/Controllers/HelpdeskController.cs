using HRMS.DB;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Helpdesk.Helpdesk_model;

namespace HRMS_API.Controllers
{
    public class HelpdeskController : ApiController
    {
  
        public GlobalRepository _globalrepository { get; set; }
        public HelpdeskRepository _helpdeskrepository { get; set; }

        public HelpdeskController()
        { 
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_helpdeskrepository == null) { _helpdeskrepository = new HelpdeskRepository(); }
        }



        [Route("api/Helpdesk/GetHelpdeskRecord/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetHelpdeskRecord(int Id)
        {
            try
            {
                HelpdeskConcern_model _obj = _helpdeskrepository.GetHelpdeskConcern(Id);

                return _obj != null
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("api/Helpdesk/GetHelpdeskRecordMonitoring/{UserId}/{UserType}/{ByConcernType}/{ConcernTypeId/{ByConcernStatus}/{ConcernStatus}/{ByDate}/{DateFrom}/{DateTo}/{Keyword}/{ByClient}/{ClientId}/{CompanyId}")]
        [HttpGet]
        public HttpResponseMessage GetHelpdeskRecordMonitoring(int UserId, string UserType,
             bool ByConcernType,  int ConcernTypeId,
             bool ByConcernStatus, string ConcernStatus, 
                bool ByDate, DateTime DateFrom, DateTime DateTo,
                string Keyword,
                bool ByClient, int ClientId, 
                int CompanyId)
        {
            try
            {
                HelpdeskMonitoringFilter_model _filter = new HelpdeskMonitoringFilter_model
                {
                    UserId = UserId,
                    UserType = UserType,
                    ByConcernType = ByConcernType,
                    ConcernTypeID = ConcernTypeId,
                    ByStatus = ByConcernStatus,
                    ConcernStatus = ConcernStatus,
                    ByDate = ByDate,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    Keyword = Keyword,
                    ByClient = ByClient,
                    ClientId = ClientId,
                    CompanyId = CompanyId
                };


                List< HelpdeskMonitoring_model> _obj = _helpdeskrepository.GetHelpdeskMonitoring(_filter);

                return _obj != null
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("api/Helpdesk/GetConcernThreads/{ConcernId}")]
        [HttpGet]
        public HttpResponseMessage GetConcernThreads(int ConcernId)
        {
            try
            {
                List<ConcernThread >_obj = _helpdeskrepository.GetConcernThreads(ConcernId);

                return _obj != null
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("api/Helpdesk/ManageHelpdeskConcern")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeFloating([FromBody] HelpdeskConcern_model model)
        {
            try
            {
                int _id = _helpdeskrepository.ManageHelpdeskConcern(model);

                if (_id  != 0) { return Request.CreateResponse(HttpStatusCode.OK, model.Id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

        }

        [Route("api/Helpdesk/ManageHelpdeskConcernThread")]
        [HttpPost]
        public HttpResponseMessage ManageHelpdeskConcernThread([FromBody] HelpdeskConcernThread_model model)
        {
            try
            {
                int _id = _helpdeskrepository.ManageHelpdeskConcernThread(model);

                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, model.Id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

        }

    }
}
