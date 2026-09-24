using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.MasterFile.DocumentTypeModel;
using static HRModel.ViewModel.Global.MasterFile.EmployeeTypeModel;
using static HRModel.ViewModel.Global.MasterFile.ShiftModel;
using static HRMS_API.Repository.MasterFileRepository;

namespace HRMS_API.Controllers.MasterFile
{
    [BasicAuthentication]

    public class DocumentTypeController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private DocumentType_repository _masterrepository { get; set; }

        public DocumentTypeController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_masterrepository == null) { _masterrepository = new DocumentType_repository(); }
        }

        [Route("api/DocumentType/Get")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                List<DocumentType_list_model> _model = _masterrepository.Get();
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/DocumentType/Get/{Id}")]
        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            try
            {
                DocumentType_model _model = _masterrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/DocumentType/Manage")]
        [HttpPost]
        public HttpResponseMessage Manage([FromBody] DocumentType_model model)
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
