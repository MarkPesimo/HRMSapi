using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.GlobalSearch_models;
using static HRModel.ViewModel.Global.MasterFile;
using static HRMS_API.Repository.MasterFileRepository;

namespace HRMS_API.Controllers.MasterFile
{
    [BasicAuthentication]
    public class HolidayController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private Holiday_repository _masterrepository { get; set; }

        public HolidayController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_masterrepository == null) { _masterrepository = new Holiday_repository(); }
        }

        [Route("api/Holiday/Get")]
        [HttpGet]
        public HttpResponseMessage Get(int? year = null)
        {
            try
            {
                List<HolidayList_model> _model = _masterrepository.Get(year);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Holiday/Manage")]
        [HttpPost]
        public HttpResponseMessage ManageLocalHoliday([FromBody] LocalHoliday_Input_model model)
        {
            try
            {
                if (model == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid input data.");
                }

                int result = _masterrepository.ManageLocalHoliday(model);

                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record.");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Holiday/GetLocalHolidayDetails")]
        [HttpGet]
        public HttpResponseMessage GetLocalHolidayDetails(int holidayId)
        {
            try
            {
                var data = _masterrepository.GetLocalHolidayDetails(holidayId);

                if (data != null && data.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No local holiday detail records found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [Route("api/Holiday/GetClientsByCompany/{_companyid}")]
        [HttpGet]
        public HttpResponseMessage GetClientsByCompany(int _companyid)
        {
            try
            {
                List<ClientListPerCompanyModel> _model = _globalrepository.GetClientsByCompany(_companyid);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }


        [Route("api/Holiday/GetEmployeesByClient/{_client_id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesByClient(int _client_id)
        {
            try
            {
                List<EmployeeListModel> _model = _globalrepository.GetEmployeesByClient(_client_id);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Holiday/ManageLocalHolidayBulkInsert")]
        [HttpPost]
        public HttpResponseMessage ManageLocalHolidayBulkInsert([FromBody] List<LocalHolidayDetail_Input_model> modelList)
        {
            try
            {
                int totalProcessed = _masterrepository.ManageLocalHolidayBulkInsert(modelList);
                return Request.CreateResponse(HttpStatusCode.OK, totalProcessed);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}