using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.MasterFile.EmployeeTypeModel;
using static HRModel.ViewModel.Global.MasterFile.ShiftModel;
using static HRMS_API.Repository.MasterFileRepository;

namespace HRMS_API.Controllers.MasterFile
{
    [BasicAuthentication]

    public class ShiftController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private Shift_repository _masterrepository { get; set; }

        public ShiftController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_masterrepository == null) { _masterrepository = new Shift_repository(); }
        }

        [Route("api/Shift/Get")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                List<Shift_list_model> _model = _masterrepository.Get();
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Shift/Get/{Id}")]
        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            try
            {
                Shift_model _model = _masterrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Shift/Manage")]
        [HttpPost]
        public HttpResponseMessage Manage([FromBody] Shift_model model)
        {
            try
            {
                int result = _masterrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}
