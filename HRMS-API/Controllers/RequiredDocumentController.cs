using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Employees.Document.RequiredDocument_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class RequiredDocumentController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private RequiredDocumentRepository _requireddocumentrepository { get; set; }

        public RequiredDocumentController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_requireddocumentrepository == null) { _requireddocumentrepository = new RequiredDocumentRepository(); }
        }

        [Route("api/RequiredDocument/GetMonitoring/{ClientId}")]
        [HttpGet]
        public HttpResponseMessage GetMonitoring(int ClientId)
        {
            try
            {
                List<Monitoring_model> _model = _requireddocumentrepository.GetMonitoring(ClientId);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/RequiredDocuments")]
        [HttpGet]
        public HttpResponseMessage RequiredDocuments()
        {
            try
            {
                List< RequiredDocument_list_model> _model = _requireddocumentrepository.GetRequiredDocuments();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/RequiredDocument/{Id}")]
        [HttpGet]
        public HttpResponseMessage RequiredDocument(int Id)
        {
            try
            {
                RequiredDocument _model = _requireddocumentrepository.GetRequiredDocument(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/ManageRequiredDocument")]
        [HttpPost]
        public HttpResponseMessage Manage([FromBody] RequiredDocument model)
        {
            try
            {
                int result = _requireddocumentrepository.ManageRequiredDocument(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/SimilarDocuments/{Id}")]
        [HttpGet]
        public HttpResponseMessage SimilarDocuments(int Id)
        {
            try
            {
                List<SimilarDocument> _model = _requireddocumentrepository.GetSimilarDocuments(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/SimilarDocument/{Id}")]
        [HttpGet]
        public HttpResponseMessage SimilarDocument(int Id)
        {
            try
            {
                SimilarDocument _model = _requireddocumentrepository.GetSimilarDocument(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/RequiredDocument/ManageSimilarDocument")]
        [HttpPost]
        public HttpResponseMessage ManageSimilarDocument([FromBody] SimilarDocument model)
        {
            try
            {            
                int result = _requireddocumentrepository.ManageSimilarDocument(model);
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
